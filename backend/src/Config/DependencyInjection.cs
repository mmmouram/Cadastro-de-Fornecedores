using Microsoft.Extensions.DependencyInjection;
using myApp.Services;
using myApp.Repositories;

namespace myApp.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            return services;
        }
    }
}
