using System.Reflection;
using Challenge.Business.Behaviors;
using Challenge.Business.Features.Generic.Delete;
using Challenge.Business.Features.Generic.GetAll;
using Challenge.Business.Features.Generic.GetById;
using Challenge.Business.Services;
using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using Challenge.Data.Entities;
using FluentValidation;
using MediatR;
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

        // Registrar handlers genéricos cerrados para entidades específicas
        // MediatR 12+ requiere registro explícito de versiones cerradas de handlers genéricos
        RegisterClosedGenericHandlers<Client>(services);
        RegisterClosedGenericHandlers<Dog>(services);
        RegisterClosedGenericHandlers<Walk>(services);
        RegisterClosedGenericHandlers<User>(services);

        // FluentValidation - Registrar todos los validators del assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // AutoMapper - Registrar todos los profiles del assembly
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Business Services
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }

    /// <summary>
    /// Registra handlers genéricos cerrados para una entidad específica
    /// </summary>
    private static void RegisterClosedGenericHandlers<TEntity>(IServiceCollection services)
        where TEntity : class, IIdentifier
    {
        // GetAll
        services.AddTransient<IRequestHandler<GetAllQuery<TEntity>, Result<List<TEntity>>>,
            GetAllQueryHandler<TEntity>>();

        // GetById
        services.AddTransient<IRequestHandler<GetByIdQuery<TEntity>, Result<TEntity?>>,
            GetByIdQueryHandler<TEntity>>();

        // Delete
        services.AddTransient<IRequestHandler<DeleteCommand<TEntity>, Result<bool>>,
            DeleteCommandHandler<TEntity>>();
    }
}
