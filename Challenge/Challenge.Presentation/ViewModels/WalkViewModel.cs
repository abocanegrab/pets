namespace Challenge.Presentation.ViewModels;

/// <summary>
/// ViewModel para mostrar información de paseos en la UI
/// </summary>
public class WalkViewModel
{
    public int Id { get; set; }
    public int DogId { get; set; }
    public string DogName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public DateTime WalkDate { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Distance { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string WalkedByUsername { get; set; } = string.Empty;

    // Para DataGridView
    public string DisplayInfo => $"{DogName} - {WalkDate:dd/MM/yyyy HH:mm} - {DurationMinutes} min";
    public string DistanceDisplay => $"{Distance:F2} km";
    public string DateDisplay => WalkDate.ToString("dd/MM/yyyy HH:mm");
}
