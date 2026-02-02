using Challenge.Core.Common;
using Challenge.Core.Exceptions;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Walk.Update;

/// <summary>
/// Handler para actualizar un paseo existente
/// </summary>
public class UpdateWalkCommandHandler : IRequestHandler<UpdateWalkCommand, Result<bool>>
{
    private readonly WriteDbContext _context;
    private readonly ILogger<UpdateWalkCommandHandler> _logger;

    public UpdateWalkCommandHandler(
        WriteDbContext context,
        ILogger<UpdateWalkCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(UpdateWalkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Actualizando paseo ID: {WalkId}", request.Id);

        var walk = await _context.Walks
            .FirstOrDefaultAsync(w => w.Id == request.Id && w.IsActive, cancellationToken);

        if (walk == null)
        {
            throw DomainException.NotFound("WALK_NOT_FOUND", $"Paseo con ID {request.Id} no encontrado");
        }

        // Verificar que el perro existe si cambió
        if (walk.DogId != request.DogId)
        {
            var dogExists = await _context.Dogs
                .AnyAsync(d => d.Id == request.DogId && d.IsActive, cancellationToken);

            if (!dogExists)
            {
                throw DomainException.NotFound("DOG_NOT_FOUND", $"Perro con ID {request.DogId} no encontrado");
            }
        }

        // Verificar que el usuario existe si cambió
        if (walk.WalkedByUserId != request.WalkedByUserId)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Id == request.WalkedByUserId && u.IsActive, cancellationToken);

            if (!userExists)
            {
                throw DomainException.NotFound("USER_NOT_FOUND", $"Usuario con ID {request.WalkedByUserId} no encontrado");
            }
        }

        walk.DogId = request.DogId;
        walk.WalkDate = request.WalkDate;
        walk.DurationMinutes = request.DurationMinutes;
        walk.Distance = request.Distance;
        walk.Notes = request.Notes;
        walk.WalkedByUserId = request.WalkedByUserId;

        _context.Walks.Update(walk);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("Paseo actualizado exitosamente: {WalkId}", walk.Id);

        return Result<bool>.Success(true);
    }
}
