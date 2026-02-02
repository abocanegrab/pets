namespace Challenge.Data.Entities;

public class Client : Person
{
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    // Navigation property
    public virtual ICollection<Dog> Dogs { get; set; } = new List<Dog>();
}
