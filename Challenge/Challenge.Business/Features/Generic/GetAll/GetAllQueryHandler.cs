using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using Challenge.Data.Context;
using Challenge.Data.Entities;
using Challenge.Models.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Generic.GetAll;

/// <summary>
/// Handler genérico para obtener todas las entidades con paginación
/// Si la entidad hereda de BaseEntity, filtra por IsActive = true
/// </summary>
public class GetAllQueryHandler<TEntity> : IRequestHandler<GetAllQuery<TEntity>, Result<PagedResponse<TEntity>>>
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

    public async Task<Result<PagedResponse<TEntity>>> Handle(GetAllQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entityName = typeof(TEntity).Name;
        _logger.LogInformation("Consultando entidades de tipo {EntityName} - Página {PageNumber}, Tamaño {PageSize}",
            entityName, request.PageNumber, request.PageSize);

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

        // Obtener conteo total
        var totalCount = await query.CountAsync(cancellationToken);

        // Si IgnorePagination está activado, retornar todos
        List<TEntity> items;
        if (request.IgnorePagination)
        {
            items = await query.ToListAsync(cancellationToken);
            _logger.LogInformation("Se retornaron {Count} entidades de tipo {EntityName} (sin paginación)",
                items.Count, entityName);
        }
        else
        {
            // Aplicar paginación
            items = await query
                .Skip(request.Skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Se retornaron {Count} de {TotalCount} entidades de tipo {EntityName} - Página {PageNumber}/{TotalPages}",
                items.Count, totalCount, entityName, request.PageNumber,
                (int)Math.Ceiling(totalCount / (double)request.PageSize));
        }

        var pagedResponse = new PagedResponse<TEntity>(items, totalCount, request.PageNumber, request.PageSize);

        return Result<PagedResponse<TEntity>>.Success(pagedResponse);
    }
}
