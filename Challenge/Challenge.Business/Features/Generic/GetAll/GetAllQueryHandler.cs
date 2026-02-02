using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using Challenge.Data.Context;
using Challenge.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Generic.GetAll;

/// <summary>
/// Handler genérico para obtener todas las entidades
/// Si la entidad hereda de BaseEntity, filtra por IsActive = true
/// </summary>
public class GetAllQueryHandler<TEntity> : IRequestHandler<GetAllQuery<TEntity>, Result<List<TEntity>>>
    where TEntity : class, IIdentifier
{
    private readonly ReadDbContext _context;
    private readonly ILogger<GetAllQueryHandler<TEntity>> _logger;

    public GetAllQueryHandler(
        ReadDbContext context,
        ILogger<GetAllQueryHandler<TEntity>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<List<TEntity>>> Handle(GetAllQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entityName = typeof(TEntity).Name;
        _logger.LogInformation("Consultando todas las entidades de tipo {EntityName}", entityName);

        IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

        // Aplicar includes si se especificaron
        if (request.Includes != null && request.Includes.Length > 0)
        {
            foreach (var include in request.Includes)
            {
                query = query.Include(include);
            }
        }

        // Si la entidad hereda de BaseEntity, filtrar por IsActive
        if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => ((BaseEntity)(object)e).IsActive);
        }

        var entities = await query.ToListAsync(cancellationToken);

        _logger.LogInformation("Se encontraron {Count} entidades de tipo {EntityName}", entities.Count, entityName);

        return Result<List<TEntity>>.Success(entities);
    }
}
