using Challenge.Core.Enums;

namespace Challenge.Data.Entities;

public abstract class Person : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public PersonType PersonType { get; set; }
}
