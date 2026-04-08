using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Persistence;
using TaskFlow.Infrastructure.Persistence.Repositories;

namespace TaskFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsql => npgsql.MigrationsAssembly(
                    typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}