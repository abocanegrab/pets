using Challenge.Presentation.ViewModels;

namespace Challenge.Presentation.Views.Dog;

/// <summary>
/// Interface que define el contrato entre la View y el Presenter para gestión de perros
/// La View es "tonta" - solo renderiza y notifica al Presenter
/// </summary>
public interface IDogView
{
    // Propiedades para los campos del formulario
    int? SelectedClientId { get; set; }
    string DogName { get; set; }
    string Breed { get; set; }
    string Age { get; set; }
    string Weight { get; set; }
    string SpecialInstructions { get; set; }
    string SearchText { get; set; }

    // Perro seleccionado actualmente
    int? SelectedDogId { get; set; }

    // Métodos para actualizar la UI
    void LoadDogs(IEnumerable<DogViewModel> dogs);
    void LoadClients(IEnumerable<(int Id, string Name)> clients);
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
    event EventHandler<int> DogSelected;
}
