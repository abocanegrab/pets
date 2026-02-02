using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;

namespace Challenge.Business.Features.Generic.GetById;

/// <summary>
/// Query genérica para obtener una entidad por ID
/// </summary>
public class GetByIdQuery<TEntity> : IRequest<Result<TEntity?>>
    where TEntity : class, IIdentifier
{
    public int Id { get; set; }

    public GetByIdQuery(int id)
    {
        Id = id;
    }
}
