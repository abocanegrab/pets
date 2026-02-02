using Challenge.Core.Common;
using Challenge.Core.Exceptions;
using Challenge.Core.Interfaces;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Generic.Delete;

/// <summary>
/// Handler genérico para eliminar (hard delete) una entidad por ID
/// </summary>
public class DeleteCommandHandler<TEntity> : IRequestHandler<DeleteCommand<TEntity>, Result<bool>>
    where TEntity : class, IIdentifier
{
    private readonly WriteDbContext _context;
    private readonly ILogger<DeleteCommandHandler<TEntity>> _logger;

    public DeleteCommandHandler(
        WriteDbContext context,
        ILogger<DeleteCommandHandler<TEntity>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(DeleteCommand<TEntity> request, CancellationToken cancellationToken)
    {
        var entityName = typeof(TEntity).Name;
        _logger.LogInformation("Eliminando {EntityName} con ID: {Id}", entityName, request.Id);

        var entity = await _context.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw DomainException.NotFound(
                $"{entityName.ToUpper()}_NOT_FOUND",
                $"{entityName} con ID {request.Id} no encontrado");
        }

        _context.Set<TEntity>().Remove(entity);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("{EntityName} eliminado exitosamente: {Id}", entityName, request.Id);

        return Result<bool>.Success(true);
    }
}
