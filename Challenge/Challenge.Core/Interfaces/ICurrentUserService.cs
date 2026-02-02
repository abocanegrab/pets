namespace Challenge.Core.Interfaces;

/// <summary>
/// Servicio para obtener información del usuario autenticado actualmente
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// ID del usuario actual (0 si no hay usuario autenticado o es una operación del sistema)
    /// </summary>
    int UserId { get; }

    /// <summary>
    /// Nombre de usuario actual (null si no hay usuario autenticado)
    /// </summary>
    string? Username { get; }

    /// <summary>
    /// Indica si hay un usuario autenticado
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Establece el usuario actual después del login
    /// </summary>
    void SetCurrentUser(int userId, string username);

    /// <summary>
    /// Limpia el usuario actual (logout)
    /// </summary>
    void Clear();
}
