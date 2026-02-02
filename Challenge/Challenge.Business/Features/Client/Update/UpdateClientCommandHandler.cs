using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateClientCommandHandler> _logger;

    public UpdateClientCommandHandler(
        WriteDbContext context,
        IMapper mapper,
        ILogger<UpdateClientCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
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

        _mapper.Map(request, client);

        _context.Clients.Update(client);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("Cliente actualizado exitosamente: {ClientId}", client.Id);

        return Result<bool>.Success(true);
    }
}
