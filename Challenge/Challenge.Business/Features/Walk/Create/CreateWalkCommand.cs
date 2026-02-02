using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;

namespace Challenge.Business.Features.Walk.Create;

/// <summary>
/// Comando para crear un nuevo paseo
/// </summary>
public class CreateWalkCommand : IRequest<Result<int>>, ITransactionalCommand
{
    public int DogId { get; set; }
    public DateTime WalkDate { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Distance { get; set; }
    public string? Notes { get; set; }
    public int WalkedByUserId { get; set; }
}
