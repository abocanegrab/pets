using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Generic.GetById;

/// <summary>
/// Handler genérico para obtener una entidad por ID (read-only, sin tracking)
/// </summary>
public class GetByIdQueryHandler<TEntity> : IRequestHandler<GetByIdQuery<TEntity>, Result<TEntity?>>
    where TEntity : class, IIdentifier
{
    private readonly ReadDbContext _context;
    private readonly ILogger<GetByIdQueryHandler<TEntity>> _logger;

    public GetByIdQueryHandler(
        ReadDbContext context,
        ILogger<GetByIdQueryHandler<TEntity>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<TEntity?>> Handle(GetByIdQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entityName = typeof(TEntity).Name;
        _logger.LogInformation("Consultando {EntityName} con ID: {Id}", entityName, request.Id);

        var entity = await _context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        return Result<TEntity?>.Success(entity);
    }
}
