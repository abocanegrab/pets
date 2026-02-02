using Challenge.Core.Common;
using Challenge.Core.Exceptions;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Client.Update;

/// <summary>
/// Handler para actualizar un cliente existente
/// </summary>
public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<bool>>
{
    private readonly WriteDbContext _context;
    private readonly ILogger<UpdateClientCommandHandler> _logger;

    public UpdateClientCommandHandler(
        WriteDbContext context,
        ILogger<UpdateClientCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Actualizando cliente ID: {ClientId}", request.Id);

        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == request.Id && c.IsActive, cancellationToken);

        if (client == null)
        {
            throw DomainException.NotFound("CLIENT_NOT_FOUND", $"Cliente con ID {request.Id} no encontrado");
        }

        client.FirstName = request.FirstName;
        client.LastName = request.LastName;
        client.PhoneNumber = request.PhoneNumber;
        client.Email = request.Email;
        client.Address = request.Address;
        client.City = request.City;
        client.State = request.State;
        client.ZipCode = request.ZipCode;

        _context.Clients.Update(client);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("Cliente actualizado exitosamente: {ClientId}", client.Id);

        return Result<bool>.Success(true);
    }
}
