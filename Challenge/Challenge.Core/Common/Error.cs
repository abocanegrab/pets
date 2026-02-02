using Challenge.Core.Enums;

namespace Challenge.Core.Common;

public class Error
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ErrorType Type { get; set; }
    public Dictionary<string, string[]>? ValidationErrors { get; set; }

    public Error() { }

    public Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error Validation(string code, string message, Dictionary<string, string[]>? validationErrors = null)
    {
        return new Error(code, message, ErrorType.Validation)
        {
            ValidationErrors = validationErrors
        };
    }

    public static Error NotFound(string code, string message)
    {
        return new Error(code, message, ErrorType.NotFound);
    }

    public static Error Conflict(string code, string message)
    {
        return new Error(code, message, ErrorType.Conflict);
    }

    public static Error Unauthorized(string code, string message)
    {
        return new Error(code, message, ErrorType.Unauthorized);
    }

    public static Error Forbidden(string code, string message)
    {
        return new Error(code, message, ErrorType.Forbidden);
    }

    public static Error Internal(string code, string message)
    {
        return new Error(code, message, ErrorType.Internal);
    }
}
