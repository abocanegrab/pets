using Challenge.Core.Interfaces;
using Challenge.Data.Context;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Behaviors;

/// <summary>
/// Pipeline behavior que ejecuta comandos dentro de una transacción de base de datos.
/// Solo se aplica a comandos que implementen ITransactionalCommand.
/// </summary>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly WriteDbContext _context;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(
        WriteDbContext context,
        ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Solo aplicar transacción si el comando implementa ITransactionalCommand
        if (request is not ITransactionalCommand)
        {
            return await next();
        }

        var commandName = typeof(TRequest).Name;
        _logger.LogInformation("Iniciando transacción para comando: {CommandName}", commandName);

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var response = await next();

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Transacción completada exitosamente para comando: {CommandName}", commandName);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en transacción para comando: {CommandName}. Ejecutando rollback.", commandName);
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
