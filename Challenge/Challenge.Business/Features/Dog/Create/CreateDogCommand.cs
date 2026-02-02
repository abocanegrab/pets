using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;

namespace Challenge.Business.Features.Dog.Create;

/// <summary>
/// Comando para crear un nuevo perro
/// </summary>
public class CreateDogCommand : IRequest<Result<int>>, ITransactionalCommand
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal? Weight { get; set; }
    public string? SpecialInstructions { get; set; }
}
