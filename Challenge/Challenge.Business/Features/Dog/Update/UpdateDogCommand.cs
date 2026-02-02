using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;

namespace Challenge.Business.Features.Dog.Update;

/// <summary>
/// Comando para actualizar un perro existente
/// </summary>
public class UpdateDogCommand : IRequest<Result<bool>>, ITransactionalCommand
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal? Weight { get; set; }
    public string? SpecialInstructions { get; set; }
}
