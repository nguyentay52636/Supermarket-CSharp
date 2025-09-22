using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace SupermarketAPI.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Supermarket API",
                    Version = "v1",
                    Description = "API cho hệ thống siêu thị"
                });
            });

            return services;
        }

        // Cấu hình Middleware cho Swagger
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API v1");
                c.RoutePrefix = "swagger";
            });

            return app;
        }
    }
}
