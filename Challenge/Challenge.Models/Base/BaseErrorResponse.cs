using Challenge.Core.Common;

namespace Challenge.Models.Base;

public class BaseErrorResponse
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string[]>? ValidationErrors { get; set; }

    public BaseErrorResponse()
    {
    }

    public BaseErrorResponse(Error error)
    {
        Code = error.Code;
        Message = error.Message;
        ValidationErrors = error.ValidationErrors;
    }

    public static BaseErrorResponse FromError(Error error)
    {
        return new BaseErrorResponse(error);
    }
}
