namespace Challenge.Core.Interfaces;

public interface IModified
{
    DateTime UpdatedAt { get; set; }
    int UpdatedBy { get; set; }
}
