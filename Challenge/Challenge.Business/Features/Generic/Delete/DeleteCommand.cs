using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;

namespace Challenge.Business.Features.Generic.Delete;

/// <summary>
/// Comando genérico para eliminar (hard delete) una entidad por ID
/// </summary>
public class DeleteCommand<TEntity> : IRequest<Result<bool>>, ITransactionalCommand
    where TEntity : class, IIdentifier
{
    public int Id { get; set; }

    public DeleteCommand(int id)
    {
        Id = id;
    }
}
