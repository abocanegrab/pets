using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;

namespace Challenge.Business.Features.Generic.GetAll;

/// <summary>
/// Query genérica para obtener todas las entidades activas
/// </summary>
public class GetAllQuery<TEntity> : IRequest<Result<List<TEntity>>>
    where TEntity : class, IIdentifier
{
}
