namespace Challenge.Core.Constants;

public static class ErrorCodes
{
    // General
    public const string ValidationError = "VALIDATION_ERROR";
    public const string NotFound = "NOT_FOUND";
    public const string InternalError = "INTERNAL_ERROR";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string Forbidden = "FORBIDDEN";

    // Client
    public const string ClientNotFound = "CLIENT_NOT_FOUND";
    public const string ClientCreateError = "CLIENT_CREATE_ERROR";
    public const string ClientUpdateError = "CLIENT_UPDATE_ERROR";
    public const string ClientDeleteError = "CLIENT_DELETE_ERROR";

    // Dog
    public const string DogNotFound = "DOG_NOT_FOUND";
    public const string DogCreateError = "DOG_CREATE_ERROR";
    public const string DogUpdateError = "DOG_UPDATE_ERROR";
    public const string DogDeleteError = "DOG_DELETE_ERROR";

    // Walk
    public const string WalkNotFound = "WALK_NOT_FOUND";
    public const string WalkCreateError = "WALK_CREATE_ERROR";
    public const string WalkUpdateError = "WALK_UPDATE_ERROR";
    public const string WalkDeleteError = "WALK_DELETE_ERROR";

    // Authentication
    public const string AuthenticationFailed = "AUTH_FAILED";
    public const string AccountLocked = "ACCOUNT_LOCKED";
    public const string InvalidCredentials = "INVALID_CREDENTIALS";
    public const string UserNotFound = "USER_NOT_FOUND";
}
