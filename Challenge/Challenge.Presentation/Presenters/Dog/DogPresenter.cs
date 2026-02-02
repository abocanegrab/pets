using Challenge.Business.Features.Dog.Create;
using Challenge.Business.Features.Dog.Update;
using Challenge.Business.Features.Generic.Delete;
using Challenge.Business.Features.Generic.GetAll;
using Challenge.Business.Features.Generic.GetById;
using Challenge.Core.Exceptions;
using Challenge.Presentation.ViewModels;
using Challenge.Presentation.Views.Dog;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Challenge.Presentation.Presenters.Dog;

/// <summary>
/// Presenter que maneja toda la lógica de DogManagementForm
/// La View solo notifica eventos, el Presenter decide qué hacer
/// </summary>
public class DogPresenter : IDisposable
{
    private readonly IDogView _view;
    private readonly IMediator _mediator;
    private readonly ILogger<DogPresenter> _logger;
    private CancellationTokenSource? _cts;
    private int _currentPage = 1;
    private const int PageSize = 20;

    public DogPresenter(IDogView view, IMediator mediator, ILogger<DogPresenter> logger)
    {
        _view = view;
        _mediator = mediator;
        _logger = logger;

        // Suscribirse a los eventos de la View
        _view.LoadRequested += OnLoadRequested;
        _view.SaveRequested += OnSaveRequested;
        _view.DeleteRequested += OnDeleteRequested;
        _view.ClearRequested += OnClearRequested;
        _view.SearchRequested += OnSearchRequested;
        _view.DogSelected += OnDogSelected;
        _view.PreviousPageRequested += OnPreviousPageRequested;
        _view.NextPageRequested += OnNextPageRequested;
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private CancellationToken GetCancellationToken()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        return _cts.Token;
    }

    public async Task InitializeAsync()
    {
        await LoadClientsAsync();
        await LoadDogsAsync();
    }

    private async void OnLoadRequested(object? sender, EventArgs e)
    {
        await LoadDogsAsync();
    }

    private async Task LoadDogsAsync()
    {
        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetAllQuery<Data.Entities.Dog>("Client")
            {
                PageSize = PageSize,
                PageNumber = _currentPage
            };
            var result = await _mediator.Send(query, ct);

            var viewModels = result.Data.Items.Select(d => new DogViewModel
            {
                Id = d.Id,
                ClientId = d.ClientId,
                ClientName = d.Client != null ? $"{d.Client.FirstName} {d.Client.LastName}" : "N/A",
                Name = d.Name,
                Breed = d.Breed,
                Age = d.Age,
                Weight = d.Weight,
                SpecialInstructions = d.SpecialInstructions ?? string.Empty
            }).ToList();

            _view.LoadDogs(viewModels);
            _view.UpdatePaginationInfo(
                result.Data.PageNumber,
                result.Data.TotalPages,
                result.Data.TotalCount,
                result.Data.HasPreviousPage,
                result.Data.HasNextPage);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Carga de perros cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar perros");
            _view.ShowMessage("Error al cargar perros: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.ShowLoading(false);
        }
    }

    private async Task LoadClientsAsync()
    {
        var ct = GetCancellationToken();

        try
        {
            var query = new GetAllQuery<Data.Entities.Client>
            {
                IgnorePagination = true  // Load all for ComboBox
            };
            var result = await _mediator.Send(query, ct);

            var clients = result.Data.Items.Select(c => (c.Id, Name: $"{c.FirstName} {c.LastName}")).ToList();
            _view.LoadClients(clients);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Carga de clientes cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar clientes");
            _view.ShowMessage("Error al cargar clientes: " + ex.Message, "Error", true);
        }
    }

    private async void OnSaveRequested(object? sender, EventArgs e)
    {
        var ct = GetCancellationToken();

        try
        {
            _view.EnableForm(false);
            _view.ShowLoading(true);

            // Validaciones básicas antes de enviar el comando
            if (!_view.SelectedClientId.HasValue)
            {
                _view.ShowMessage("Por favor seleccione un cliente", "Validación", true);
                return;
            }

            if (!int.TryParse(_view.Age, out int age))
            {
                _view.ShowMessage("Por favor ingrese una edad válida", "Validación", true);
                return;
            }

            decimal? weight = null;
            if (!string.IsNullOrWhiteSpace(_view.Weight))
            {
                if (!decimal.TryParse(_view.Weight, out decimal parsedWeight))
                {
                    _view.ShowMessage("Por favor ingrese un peso válido", "Validación", true);
                    return;
                }
                weight = parsedWeight;
            }

            if (_view.SelectedDogId.HasValue)
            {
                // Update
                var updateCommand = new UpdateDogCommand
                {
                    Id = _view.SelectedDogId.Value,
                    ClientId = _view.SelectedClientId.Value,
                    Name = _view.DogName,
                    Breed = _view.Breed,
                    Age = age,
                    Weight = weight,
                    SpecialInstructions = _view.SpecialInstructions
                };

                await _mediator.Send(updateCommand, ct);
                _view.ShowMessage("Perro actualizado exitosamente", "Éxito", false);
            }
            else
            {
                // Create
                var createCommand = new CreateDogCommand
                {
                    ClientId = _view.SelectedClientId.Value,
                    Name = _view.DogName,
                    Breed = _view.Breed,
                    Age = age,
                    Weight = weight,
                    SpecialInstructions = _view.SpecialInstructions
                };

                await _mediator.Send(createCommand, ct);
                _view.ShowMessage("Perro creado exitosamente", "Éxito", false);
            }

            _view.ClearForm();
            await LoadDogsAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Guardado de perro cancelado");
        }
        catch (DomainException ex)
        {
            var errorMessage = FormatValidationErrors(ex);
            _view.ShowMessage(errorMessage, "Error de Validación", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar perro");
            _view.ShowMessage("Error al guardar perro: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.EnableForm(true);
            _view.ShowLoading(false);
        }
    }

    private async void OnDeleteRequested(object? sender, EventArgs e)
    {
        if (!_view.SelectedDogId.HasValue)
        {
            _view.ShowMessage("Por favor seleccione un perro para eliminar", "Advertencia", true);
            return;
        }

        var ct = GetCancellationToken();

        try
        {
            _view.EnableForm(false);
            _view.ShowLoading(true);

            var deleteCommand = new DeleteCommand<Data.Entities.Dog>(_view.SelectedDogId.Value);
            await _mediator.Send(deleteCommand, ct);

            _view.ShowMessage("Perro eliminado exitosamente", "Éxito", false);
            _view.ClearForm();
            await LoadDogsAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Eliminación de perro cancelada");
        }
        catch (DomainException ex)
        {
            _view.ShowMessage(ex.Message, "Error", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar perro");
            _view.ShowMessage("Error al eliminar perro: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.EnableForm(true);
            _view.ShowLoading(false);
        }
    }

    private void OnClearRequested(object? sender, EventArgs e)
    {
        _view.ClearForm();
    }

    private async void OnSearchRequested(object? sender, EventArgs e)
    {
        // Si el search está vacío, cargar todos
        if (string.IsNullOrWhiteSpace(_view.SearchText))
        {
            await LoadDogsAsync();
            return;
        }

        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetAllQuery<Data.Entities.Dog>("Client")
            {
                IgnorePagination = true  // Search needs all results to filter
            };
            var result = await _mediator.Send(query, ct);

            // Filtrar en el cliente
            var searchText = _view.SearchText.ToLower();
            var filtered = result.Data.Items
                .Where(d =>
                    d.Name.ToLower().Contains(searchText) ||
                    d.Breed.ToLower().Contains(searchText) ||
                    (d.Client != null &&
                     (d.Client.FirstName.ToLower().Contains(searchText) ||
                      d.Client.LastName.ToLower().Contains(searchText))))
                .Select(d => new DogViewModel
                {
                    Id = d.Id,
                    ClientId = d.ClientId,
                    ClientName = d.Client != null ? $"{d.Client.FirstName} {d.Client.LastName}" : "N/A",
                    Name = d.Name,
                    Breed = d.Breed,
                    Age = d.Age,
                    Weight = d.Weight,
                    SpecialInstructions = d.SpecialInstructions ?? string.Empty
                })
                .ToList();

            _view.LoadDogs(filtered);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Búsqueda de perros cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar perros");
            _view.ShowMessage("Error al buscar: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.ShowLoading(false);
        }
    }

    private async void OnDogSelected(object? sender, int dogId)
    {
        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetByIdQuery<Data.Entities.Dog>(dogId);
            var result = await _mediator.Send(query, ct);

            if (result.Data != null)
            {
                _view.SelectedDogId = result.Data.Id;
                _view.SelectedClientId = result.Data.ClientId;
                _view.DogName = result.Data.Name;
                _view.Breed = result.Data.Breed;
                _view.Age = result.Data.Age.ToString();
                _view.Weight = result.Data.Weight?.ToString() ?? string.Empty;
                _view.SpecialInstructions = result.Data.SpecialInstructions ?? string.Empty;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Carga de perro cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar perro");
            _view.ShowMessage("Error al cargar perro: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.ShowLoading(false);
        }
    }

    /// <summary>
    /// Formatea los errores de validación de una DomainException para mostrarlos al usuario
    /// </summary>
    private string FormatValidationErrors(DomainException ex)
    {
        if (ex.ValidationErrors == null || !ex.ValidationErrors.Any())
        {
            return ex.Message;
        }

        var errorMessages = new System.Text.StringBuilder();
        errorMessages.AppendLine("Por favor corrija los siguientes errores:\n");

        foreach (var (field, errors) in ex.ValidationErrors)
        {
            var fieldName = TranslateFieldName(field);
            foreach (var error in errors)
            {
                errorMessages.AppendLine($"• {fieldName}: {error}");
            }
        }

        return errorMessages.ToString();
    }

    /// <summary>
    /// Traduce nombres de campos técnicos a nombres amigables
    /// </summary>
    private string TranslateFieldName(string fieldName)
    {
        return fieldName switch
        {
            "ClientId" => "Cliente",
            "Name" => "Nombre",
            "Breed" => "Raza",
            "Age" => "Edad",
            "Weight" => "Peso",
            "SpecialInstructions" => "Instrucciones Especiales",
            _ => fieldName
        };
    }

    private async void OnPreviousPageRequested(object? sender, EventArgs e)
    {
        if (_currentPage > 1)
        {
            _currentPage--;
            await LoadDogsAsync();
        }
    }

    private async void OnNextPageRequested(object? sender, EventArgs e)
    {
        _currentPage++;
        await LoadDogsAsync();
    }
}
