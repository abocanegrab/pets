using AutoMapper;
using Challenge.Core.Common;
using Challenge.Core.Enums;
using Challenge.Data.Context;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Client.Create;

/// <summary>
/// Handler para crear un nuevo cliente
/// </summary>
public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Result<int>>
{
    private readonly WriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateClientCommandHandler> _logger;

    public CreateClientCommandHandler(
        WriteDbContext context,
        IMapper mapper,
        ILogger<CreateClientCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creando cliente: {FirstName} {LastName}", request.FirstName, request.LastName);

        var client = _mapper.Map<Data.Entities.Client>(request);
        client.PersonType = PersonType.Client;
        client.IsActive = true;

        await _context.Clients.AddAsync(client, cancellationToken);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("Cliente creado exitosamente con ID: {ClientId}", client.Id);

        return Result<int>.Success(client.Id);
    }
}
