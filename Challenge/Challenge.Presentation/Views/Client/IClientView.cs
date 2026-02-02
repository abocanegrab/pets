using Challenge.Presentation.ViewModels;

namespace Challenge.Presentation.Views.Client;

/// <summary>
/// Interface que define el contrato entre la View y el Presenter
/// La View es "tonta" - solo renderiza y notifica al Presenter
/// </summary>
public interface IClientView
{
    // Propiedades para los campos del formulario
    string FirstName { get; set; }
    string LastName { get; set; }
    string PhoneNumber { get; set; }
    string Email { get; set; }
    string Address { get; set; }
    string City { get; set; }
    string State { get; set; }
    string ZipCode { get; set; }
    string SearchText { get; set; }

    // Cliente seleccionado actualmente
    int? SelectedClientId { get; set; }

    // Métodos para actualizar la UI
    void LoadClients(IEnumerable<ClientViewModel> clients);
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
    event EventHandler<int> ClientSelected;
}
