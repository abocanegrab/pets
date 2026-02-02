using Challenge.Core.Common;
using Challenge.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Auth.Logout;

/// <summary>
/// Handler para cerrar sesión
/// </summary>
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<bool>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(
        ICurrentUserService currentUserService,
        ILogger<LogoutCommandHandler> logger)
    {
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var username = _currentUserService.Username;

        _logger.LogInformation("Usuario cerrando sesión: {Username}", username);

        _currentUserService.Clear();

        _logger.LogInformation("Sesión cerrada exitosamente");

        return Task.FromResult(Result<bool>.Success(true));
    }
}
