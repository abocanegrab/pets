using Challenge.Core.Common;
using Challenge.Core.Exceptions;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Dog.Create;

/// <summary>
/// Handler para crear un nuevo perro
/// </summary>
public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, Result<int>>
{
    private readonly WriteDbContext _context;
    private readonly ILogger<CreateDogCommandHandler> _logger;

    public CreateDogCommandHandler(
        WriteDbContext context,
        ILogger<CreateDogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creando perro: {Name} para cliente ID: {ClientId}", request.Name, request.ClientId);

        // Verificar que el cliente existe
        var clientExists = await _context.Clients
            .AnyAsync(c => c.Id == request.ClientId && c.IsActive, cancellationToken);

        if (!clientExists)
        {
            throw DomainException.NotFound("CLIENT_NOT_FOUND", $"Cliente con ID {request.ClientId} no encontrado");
        }

        var dog = new Data.Entities.Dog
        {
            ClientId = request.ClientId,
            Name = request.Name,
            Breed = request.Breed,
            Age = request.Age,
            Weight = request.Weight,
            SpecialInstructions = request.SpecialInstructions,
            IsActive = true
        };

        await _context.Dogs.AddAsync(dog, cancellationToken);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("Perro creado exitosamente con ID: {DogId}", dog.Id);

        return Result<int>.Success(dog.Id);
    }
}
