using Challenge.Core.Common;
using MediatR;

namespace Challenge.Business.Features.Auth.Logout;

/// <summary>
/// Comando para cerrar sesión
/// </summary>
public class LogoutCommand : IRequest<Result<bool>>
{
}
