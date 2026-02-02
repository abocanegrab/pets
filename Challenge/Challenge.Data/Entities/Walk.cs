namespace Challenge.Data.Entities;

public class Walk : BaseEntity
{
    public int DogId { get; set; }
    public DateTime WalkDate { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Distance { get; set; }
    public string? Notes { get; set; }
    public int WalkedByUserId { get; set; }

    // Navigation properties
    public virtual Dog Dog { get; set; } = null!;
    public virtual User WalkedByUser { get; set; } = null!;
}
