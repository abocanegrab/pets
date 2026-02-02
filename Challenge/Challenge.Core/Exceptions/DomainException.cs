using Challenge.Core.Common;
using Challenge.Core.Enums;

namespace Challenge.Core.Exceptions;

public class DomainException : Exception
{
    public string Code { get; }
    public ErrorType ErrorType { get; }
    public Dictionary<string, string[]>? ValidationErrors { get; }

    public DomainException(string code, string message, ErrorType errorType = ErrorType.Internal)
        : base(message)
    {
        Code = code;
        ErrorType = errorType;
    }

    public DomainException(string code, string message, ErrorType errorType, Dictionary<string, string[]>? validationErrors)
        : base(message)
    {
        Code = code;
        ErrorType = errorType;
        ValidationErrors = validationErrors;
    }

    // Factory methods para facilitar el uso
    public static DomainException NotFound(string code, string message)
        => new DomainException(code, message, ErrorType.NotFound);

    public static DomainException Validation(string code, string message, Dictionary<string, string[]>? validationErrors = null)
        => new DomainException(code, message, ErrorType.Validation, validationErrors);

    public static DomainException Conflict(string code, string message)
        => new DomainException(code, message, ErrorType.Conflict);

    public static DomainException Unauthorized(string code, string message)
        => new DomainException(code, message, ErrorType.Unauthorized);

    public static DomainException Forbidden(string code, string message)
        => new DomainException(code, message, ErrorType.Forbidden);

    public static DomainException Internal(string code, string message)
        => new DomainException(code, message, ErrorType.Internal);

    public Error ToError()
    {
        return new Error(Code, Message, ErrorType)
        {
            ValidationErrors = ValidationErrors
        };
    }
}
