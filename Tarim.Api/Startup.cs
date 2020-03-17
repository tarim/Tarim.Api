using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Tarim.Api.Infrastructure.DataProvider;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Service;

namespace Tarim.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IConnection>(new Connection(Configuration.GetConnectionString("Tarim:Conn")));
            services.AddSingleton<INameRepository, NameRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tarim API",
                    Description = "Tarim Lab API",
                    TermsOfService = new Uri("https://koznek.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Nur Karluk",
                        Email = "nur@koznek.com",
                        Url = new Uri("https://github.com/tarim"),
                    }
                });
            });

            services.AddAuthentication().AddGoogle(
                options => {
                    IConfigurationSection googleAuth = Configuration.GetSection("Auth:Google");

                    options.ClientId = googleAuth["ClientId"];
                    options.ClientSecret = googleAuth["ClientSecret"];
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarim API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
