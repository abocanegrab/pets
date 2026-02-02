namespace Challenge.Core.Interfaces;

public interface ICreated
{
    DateTime CreatedAt { get; set; }
    int CreatedBy { get; set; }
}
