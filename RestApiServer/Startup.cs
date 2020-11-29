using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestApiServer.Interfaces;
using RestApiServer.Models;
using RestApiServer.Services;

namespace RestApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var serveSettingSection = Configuration.GetSection("ServerSettings");
            services.Configure<ServerSettings>(serveSettingSection);
            var appSettings = serveSettingSection.Get<ServerSettings>();
            services.AddSingleton(s => appSettings);

            services.AddSingleton(typeof(IRepository), typeof(Repository));
            services.AddSingleton(typeof(IHocrParser), typeof(HocrConverter));
            services.AddSingleton(typeof(IHocrService), typeof(HocrService));
            services.AddSingleton(typeof(IHocrMapper), typeof(HocrMapper));
            services.AddSingleton(typeof(IFileLoader), typeof(FileLoader));

            services.AddEntityFrameworkSqlite().AddDbContext<Repository>();

            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Rest API documentation",
                    Description = "A simple ASP.NET Core Web API"
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "RestApiServer.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
