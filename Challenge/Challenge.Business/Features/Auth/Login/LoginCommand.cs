using Challenge.Core.Common;
using MediatR;

namespace Challenge.Business.Features.Auth.Login;

/// <summary>
/// Comando para iniciar sesión
/// </summary>
public class LoginCommand : IRequest<Result<LoginResponse>>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Respuesta del login con información del usuario autenticado
/// </summary>
public class LoginResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
