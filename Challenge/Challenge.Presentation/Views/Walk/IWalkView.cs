using Challenge.Presentation.ViewModels;

namespace Challenge.Presentation.Views.Walk;

/// <summary>
/// Interface que define el contrato entre la View y el Presenter para gestión de paseos
/// La View es "tonta" - solo renderiza y notifica al Presenter
/// </summary>
public interface IWalkView
{
    // Propiedades para los campos del formulario
    int? SelectedDogId { get; set; }
    DateTime WalkDate { get; set; }
    string DurationMinutes { get; set; }
    string Distance { get; set; }
    string Notes { get; set; }
    string SearchText { get; set; }

    // Paseo seleccionado actualmente
    int? SelectedWalkId { get; set; }

    // Métodos para actualizar la UI
    void LoadWalks(IEnumerable<WalkViewModel> walks);
    void LoadDogs(IEnumerable<(int Id, string Name, string ClientName)> dogs);
    void ClearForm();
    void EnableForm(bool enabled);
    void ShowMessage(string message, string title, bool isError);
    void ShowLoading(bool show);

    // Eventos que el Presenter escuchará
    event EventHandler LoadRequested;
    event EventHandler SaveRequested;
    event EventHandler DeleteRequested;
    event EventHandler ClearRequested;
    event EventHandler SearchRequested;
    event EventHandler<int> WalkSelected;
}
