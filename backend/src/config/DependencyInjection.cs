using Microsoft.Extensions.DependencyInjection;
using Prudential.Backend.Data;
using Prudential.Backend.Repositories;
using Prudential.Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Prudential.Backend.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();

            return services;
        }
    }
}
