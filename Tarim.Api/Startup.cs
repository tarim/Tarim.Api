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
            services.AddSingleton<INameRepository, NameRepository>();
            services.AddSingleton<ITipsRepository, TipsRepository>();
            services.AddSingleton<IProverbRepository, ProverbRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IProductsRepository, ProductsRepository>();
            

            ConfigureAuthenticationSettings(services);
            services.AddMvc(options=>options.RespectBrowserAcceptHeader=true).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
                
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("This is dev environment");
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
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
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Auth:Jwt:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            })
              .AddGoogle(

                options => {

                    IConfigurationSection googleAuth = Configuration.GetSection("Auth:Google");
                    //  options.UsePkce = true;

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
                                    Enter 'Bearer' [space] and then your token in the text input below. 
                                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
            });
        }
    }
}
