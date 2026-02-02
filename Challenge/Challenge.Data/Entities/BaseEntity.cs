using Challenge.Core.Interfaces;

namespace Challenge.Data.Entities;

public abstract class BaseEntity : IIdentifier, ICreated, IModified
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public int UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
}
