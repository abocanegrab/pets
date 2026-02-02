namespace Challenge.Data.Entities;

public class User : Person
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime? LastLoginDate { get; set; }
    public bool IsLocked { get; set; } = false;

    // Navigation property
    public virtual ICollection<Walk> Walks { get; set; } = new List<Walk>();
}
