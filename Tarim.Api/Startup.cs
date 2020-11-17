using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Tarim.Api.Infrastructure.DataProvider;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Service;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tarim.Api.Infrastructure.Common.ActionFilters;
using System.IO;
using System.Reflection;

namespace Tarim.Api
{
    public class Startup
    {
        readonly string MyAllowOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;
        }
        public static IConfiguration StaticConfig { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod().AllowAnyHeader();
                });
            })
            .AddControllers();
            ConfigureSwagger(services);
            services.AddHealthChecks();
            //     services.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true);
            //  services.AddSigningCredential();
            services.AddSingleton<IConnection>(new Connection(Configuration.GetConnectionString("Tarim:Conn")));
            services.AddTransient<INameRepository, NameRepository>();
            services.AddTransient<ITipsRepository, TipsRepository>();
            services.AddTransient<IProverbRepository, ProverbRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();


            ConfigureAuthenticationSettings(services);
            services.AddMvc(options => options.RespectBrowserAcceptHeader = true).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;

                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("This is dev environment");
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarim API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors(it => it.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapControllers();
            });


        }

        //  private static void CreateIdentityIfNotCreated(IServiceCollection services)
        //  {
        //     var sp = services.BuildServiceProvider();
        //     using var scope = sp.CreateScope();
        //    var existingUserManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
        //    if (existingUserManager == null)
        //    {
        //        services.AddIdentity<AppUser, IdentityRole>()
        //                .AddEntityFrameworkStores<AppDbContext>()
        //                .AddDefaultTokenProviders();
        //    }
        // }
        private void ConfigureAuthenticationSettings(IServiceCollection services)
        {
            IConfigurationSection googleAuth = Configuration.GetSection("Auth:Google");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(googleAuth["ClientSecret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            })
              .AddGoogle(
                options =>
                {
                    options.ClientId = googleAuth["ClientId"];
                    options.ClientSecret = googleAuth["ClientSecret"];
                });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TARIM API",
                    Description = "Tarim Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Nur Karluk",
                        Email = "nur@karluks.com",
                        Url = new Uri("https://www.karluks.com/nur"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                    }
                });
                c.SchemaFilter<SwaggerExcludeFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                    Enter 'your [token] in the text input below. 
                                    Example: '12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                        },
                        new List<string>()
                      }
                    });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
