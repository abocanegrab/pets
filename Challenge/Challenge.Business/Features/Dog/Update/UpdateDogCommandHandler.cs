using AutoMapper;
using Challenge.Core.Common;
using Challenge.Core.Exceptions;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Dog.Update;

/// <summary>
/// Handler para actualizar un perro existente
/// </summary>
public class UpdateDogCommandHandler : IRequestHandler<UpdateDogCommand, Result<bool>>
{
    private readonly WriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateDogCommandHandler> _logger;

    public UpdateDogCommandHandler(
        WriteDbContext context,
        IMapper mapper,
        ILogger<UpdateDogCommandHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(UpdateDogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Actualizando perro ID: {DogId}", request.Id);

        var dog = await _context.Dogs
            .FirstOrDefaultAsync(d => d.Id == request.Id && d.IsActive, cancellationToken);

        if (dog == null)
        {
            throw DomainException.NotFound("DOG_NOT_FOUND", $"Perro con ID {request.Id} no encontrado");
        }

        // Verificar que el nuevo cliente existe si cambió
        if (dog.ClientId != request.ClientId)
        {
            var clientExists = await _context.Clients
                .AnyAsync(c => c.Id == request.ClientId && c.IsActive, cancellationToken);

            if (!clientExists)
            {
                throw DomainException.NotFound("CLIENT_NOT_FOUND", $"Cliente con ID {request.ClientId} no encontrado");
            }
        }

        _mapper.Map(request, dog);

        _context.Dogs.Update(dog);

        // SaveChanges y Commit son manejados por TransactionBehavior

        _logger.LogInformation("Perro actualizado exitosamente: {DogId}", dog.Id);

        return Result<bool>.Success(true);
    }
}
