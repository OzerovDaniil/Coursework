using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SushiManagementSystem.Infrastructure.Data;
using SushiManagementSystem.Infrastructure.Repositories;
using SushiManagementSystem.Application.Interfaces;

namespace SushiManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}