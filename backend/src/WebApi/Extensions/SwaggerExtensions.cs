using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MyApp.WebApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Fornecedores API",
                    Version = "v2",
                    Description = "API para cadastro de fornecedores com suporte a CNPJ alfanumérico."
                });
            });
            return services;
        }
    }
}