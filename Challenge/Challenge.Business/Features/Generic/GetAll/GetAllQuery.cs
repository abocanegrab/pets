using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using Challenge.Models.Base;
using MediatR;

namespace Challenge.Business.Features.Generic.GetAll;

/// <summary>
/// Query genérica para obtener todas las entidades activas con paginación
/// </summary>
public class GetAllQuery<TEntity> : PagedRequest, IRequest<Result<PagedResponse<TEntity>>>
    where TEntity : class, IIdentifier
{
    /// <summary>
    /// Propiedades de navegación a incluir (Include)
    /// </summary>
    public string[] Includes { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Si es true, ignora paginación y retorna todos los resultados
    /// </summary>
    public bool IgnorePagination { get; set; }

    public GetAllQuery() { }

    public GetAllQuery(params string[] includes)
    {
        Includes = includes;
    }
}
