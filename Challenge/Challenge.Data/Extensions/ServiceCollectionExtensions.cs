using Challenge.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Data.Extensions;

/// <summary>
/// Extensiones para configurar servicios de la capa Data
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection not found in configuration");

        // CQRS: Contextos separados para lectura y escritura
        // WriteDbContext - Para Commands (Create, Update, Delete)
        services.AddDbContext<WriteDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(connectionString);
        });

        // ReadDbContext - Para Queries (Get, List, Search)
        services.AddDbContext<ReadDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        return services;
    }
}
