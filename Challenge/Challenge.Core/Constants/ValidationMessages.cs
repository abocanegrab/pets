namespace Challenge.Core.Constants;

public static class ValidationMessages
{
    public const string Required = "{PropertyName} is required";
    public const string MaxLength = "{PropertyName} cannot exceed {MaxLength} characters";
    public const string MinLength = "{PropertyName} must be at least {MinLength} characters";
    public const string EmailInvalid = "Invalid email format";
    public const string PhoneInvalid = "Invalid phone number format";
    public const string GreaterThan = "{PropertyName} must be greater than {ComparisonValue}";
    public const string LessThan = "{PropertyName} must be less than {ComparisonValue}";
    public const string InvalidDate = "{PropertyName} must be a valid date";
    public const string FutureDate = "{PropertyName} cannot be in the future";
    public const string PastDate = "{PropertyName} cannot be in the past";
}
