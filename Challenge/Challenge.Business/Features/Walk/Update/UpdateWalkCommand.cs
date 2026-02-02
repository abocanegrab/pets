using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;

namespace Challenge.Business.Features.Walk.Update;

/// <summary>
/// Comando para actualizar un paseo existente
/// </summary>
public class UpdateWalkCommand : IRequest<Result<bool>>, ITransactionalCommand
{
    public int Id { get; set; }
    public int DogId { get; set; }
    public DateTime WalkDate { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Distance { get; set; }
    public string? Notes { get; set; }
    public int WalkedByUserId { get; set; }
}
