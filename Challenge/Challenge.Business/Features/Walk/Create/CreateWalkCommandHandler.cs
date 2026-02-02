using AutoMapper;
using Challenge.Core.Common;
using Challenge.Core.Exceptions;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Walk.Create;

/// <summary>
/// Handler para crear un nuevo paseo
/// </summary>
public class CreateWalkCommandHandler : IRequestHandler<CreateWalkCommand, Result<int>>
{
    private readonly WriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateWalkCommandHandler> _logger;

    public CreateWalkCommandHandler(
        WriteDbContext context,
        IMapper mapper,
        ILogger<CreateWalkCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreateWalkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creando paseo para perro ID: {DogId} en fecha: {WalkDate}", request.DogId, request.WalkDate);

        // Verificar que el perro existe
        var dogExists = await _context.Dogs
            .AnyAsync(d => d.Id == request.DogId && d.IsActive, cancellationToken);

        if (!dogExists)
        {
            throw DomainException.NotFound("DOG_NOT_FOUND", $"Perro con ID {request.DogId} no encontrado");
        }

        // Verificar que el usuario existe
        var userExists = await _context.Users
            .AnyAsync(u => u.Id == request.WalkedByUserId && u.IsActive, cancellationToken);

        if (!userExists)
        {
            throw DomainException.NotFound("USER_NOT_FOUND", $"Usuario con ID {request.WalkedByUserId} no encontrado");
        }

        var walk = _mapper.Map<Data.Entities.Walk>(request);
        walk.IsActive = true;

        await _context.Walks.AddAsync(walk, cancellationToken);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("Paseo creado exitosamente con ID: {WalkId}", walk.Id);

        return Result<int>.Success(walk.Id);
    }
}
