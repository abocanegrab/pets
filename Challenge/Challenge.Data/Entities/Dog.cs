namespace Challenge.Data.Entities;

public class Dog : BaseEntity
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal? Weight { get; set; }
    public string? SpecialInstructions { get; set; }

    // Navigation properties
    public virtual Client Client { get; set; } = null!;
    public virtual ICollection<Walk> Walks { get; set; } = new List<Walk>();
}
