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
    /// <summary>
    /// Propiedades de navegación a incluir (Include)
    /// </summary>
    public string[] Includes { get; set; } = Array.Empty<string>();

    public GetAllQuery() { }

    public GetAllQuery(params string[] includes)
    {
        Includes = includes;
    }
}
