using System.Reflection;
using Challenge.Business.Behaviors;
using Challenge.Business.Services;
using Challenge.Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Business.Extensions;

/// <summary>
/// Extensiones para configurar servicios de la capa Business
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // MediatR - Registrar handlers del assembly actual
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            // Pipeline behaviors (en orden de ejecución)
            // 1. ValidationBehavior - Valida el request
            // 2. TransactionBehavior - Ejecuta dentro de transacción si es ITransactionalCommand
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        // FluentValidation - Registrar todos los validators del assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // AutoMapper - Registrar todos los profiles del assembly
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Business Services
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
