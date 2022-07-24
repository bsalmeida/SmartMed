using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SmartMed.Web.Bootstrap
{
    public static class SwaggerBootstrap
    {
        private const string Version = "v1";

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(
                        Version,
                        new OpenApiInfo
                        {
                            Title = "Medications API",
                            Version = Version,
                        });
                });

            return services;
        }

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Medications API");
                    options.DocExpansion(DocExpansion.List);
                });

            return app;
        }
    }
}
