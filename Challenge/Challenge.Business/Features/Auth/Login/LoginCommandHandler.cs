using Challenge.Core.Common;
using Challenge.Core.Exceptions;
using Challenge.Core.Interfaces;
using Challenge.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Business.Features.Auth.Login;

/// <summary>
/// Handler para procesar el inicio de sesión
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly ReadDbContext _readContext;
    private readonly WriteDbContext _writeContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        ReadDbContext readContext,
        WriteDbContext writeContext,
        ICurrentUserService currentUserService,
        ILogger<LoginCommandHandler> logger)
    {
        _readContext = readContext;
        _writeContext = writeContext;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Intento de login para usuario: {Username}", request.Username);

        // Buscar usuario por username
        var user = await _readContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive, cancellationToken);

        if (user == null)
        {
            _logger.LogWarning("Login fallido: Usuario {Username} no encontrado", request.Username);
            throw DomainException.NotFound("INVALID_CREDENTIALS", "Usuario o contraseña incorrectos");
        }

        // Verificar que el usuario no está bloqueado
        if (user.IsLocked)
        {
            _logger.LogWarning("Login fallido: Usuario {Username} está bloqueado", request.Username);
            throw DomainException.Conflict("ACCOUNT_LOCKED", "La cuenta está bloqueada. Contacte al administrador.");
        }

        // Verificar la contraseña con BCrypt
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Login fallido: Contraseña incorrecta para usuario {Username}", request.Username);
            throw DomainException.NotFound("INVALID_CREDENTIALS", "Usuario o contraseña incorrectos");
        }

        // Actualizar última fecha de login (WriteDbContext)
        var userToUpdate = await _writeContext.Users
            .FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

        if (userToUpdate != null)
        {
            userToUpdate.LastLoginDate = DateTime.UtcNow;
            _writeContext.Users.Update(userToUpdate);
            await _writeContext.SaveChangesAsync(cancellationToken);
        }

        // Establecer usuario actual en el servicio
        _currentUserService.SetCurrentUser(user.Id, user.Username);

        _logger.LogInformation("Login exitoso para usuario: {Username} (ID: {UserId})", user.Username, user.Id);

        // Retornar información del usuario autenticado
        var response = new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email ?? string.Empty
        };

        return Result<LoginResponse>.Success(response);
    }
}
