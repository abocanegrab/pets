using Challenge.Core.Interfaces;

namespace Challenge.Business.Services;

/// <summary>
/// Servicio para manejar información del usuario autenticado actualmente.
/// En Windows Forms, este servicio mantiene el estado del usuario en memoria después del login.
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private int _userId;
    private string? _username;

    public int UserId => _userId;

    public string? Username => _username;

    public bool IsAuthenticated => _userId > 0;

    public void SetCurrentUser(int userId, string username)
    {
        _userId = userId;
        _username = username;
    }

    public void Clear()
    {
        _userId = 0;
        _username = null;
    }
}
