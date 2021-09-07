using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesTaxCalculatorService.Application;
using SalesTaxCalculatorService.Application.Common.Configuration;
using SalesTaxCalculatorService.Application.Common.Interfaces;
using SalesTaxCalculatorService.Infrastructure;
using SalesTaxCalculatorService.Infrastructure.Services;
using SalesTaxCalculatorService.WebApi.Common.Middleware;
using System;

namespace SalesTaxCalculatorService.WebApi
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
            services.Configure<AppSettings>(Configuration);

            services.AddApplication();
            services.AddInfrastructure();

            var taxJarSettings = new TaxJarSettings();
            Configuration.GetSection("TaxJar").Bind(taxJarSettings);
            services.AddHttpClient<ITaxCalculatorFactory, TaxCalculatorFactory>(c =>
            {
                c.BaseAddress = new Uri(taxJarSettings.BaseUrl);
                c.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Token token={taxJarSettings.ApiKey}");
            });

            AddVersioning(services);
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Add API Versioning configuration and documentation
        /// </summary>
        /// <param name="services"></param>
        private static void AddVersioning(IServiceCollection services)
        {
            // To add a new version, duplicate this with the new version number
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "V1";
                document.ApiGroupNames = new[] { "1" };
                document.Title = "Sales Tax Calculator API";
            });

            // Add API versioning
            services.AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
        }
    }
}
