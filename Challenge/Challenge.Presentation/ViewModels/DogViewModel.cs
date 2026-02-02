namespace Challenge.Presentation.ViewModels;

/// <summary>
/// ViewModel para mostrar información de perros en la UI
/// </summary>
public class DogViewModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public int Age { get; set; }
    public decimal? Weight { get; set; }
    public string SpecialInstructions { get; set; } = string.Empty;

    // Para DataGridView
    public string DisplayInfo => $"{Name} ({Breed}) - {Age} años - Cliente: {ClientName}";
    public string WeightDisplay => Weight.HasValue ? $"{Weight:F2} kg" : "N/A";
}
