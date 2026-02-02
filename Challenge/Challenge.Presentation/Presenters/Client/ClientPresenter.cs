using Challenge.Business.Features.Client.Create;
using Challenge.Business.Features.Client.Update;
using Challenge.Business.Features.Generic.Delete;
using Challenge.Business.Features.Generic.GetAll;
using Challenge.Business.Features.Generic.GetById;
using Challenge.Core.Exceptions;
using Challenge.Presentation.ViewModels;
using Challenge.Presentation.Views.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Challenge.Presentation.Presenters.Client;

/// <summary>
/// Presenter que maneja toda la lógica de ClientManagementForm
/// La View solo notifica eventos, el Presenter decide qué hacer
/// </summary>
public class ClientPresenter : IDisposable
{
    private readonly IClientView _view;
    private readonly IMediator _mediator;
    private readonly ILogger<ClientPresenter> _logger;
    private CancellationTokenSource? _cts;
    private int _currentPage = 1;
    private const int PageSize = 20;

    public ClientPresenter(IClientView view, IMediator mediator, ILogger<ClientPresenter> logger)
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
        _view.ClientSelected += OnClientSelected;
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
    }

    private async void OnLoadRequested(object? sender, EventArgs e)
    {
        await LoadClientsAsync();
    }

    private async Task LoadClientsAsync()
    {
        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetAllQuery<Data.Entities.Client>
            {
                PageSize = PageSize,
                PageNumber = _currentPage
            };
            var result = await _mediator.Send(query, ct);

            var viewModels = result.Data.Items.Select(c => new ClientViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                FullName = $"{c.FirstName} {c.LastName}",
                PhoneNumber = c.PhoneNumber,
                Email = c.Email ?? string.Empty,
                Address = c.Address,
                City = c.City,
                State = c.State,
                ZipCode = c.ZipCode
            }).ToList();

            _view.LoadClients(viewModels);
            _view.UpdatePaginationInfo(
                result.Data.PageNumber,
                result.Data.TotalPages,
                result.Data.TotalCount,
                result.Data.HasPreviousPage,
                result.Data.HasNextPage);
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
        finally
        {
            _view.ShowLoading(false);
        }
    }

    private async void OnSaveRequested(object? sender, EventArgs e)
    {
        var ct = GetCancellationToken();

        try
        {
            _view.EnableForm(false);
            _view.ShowLoading(true);

            if (_view.SelectedClientId.HasValue)
            {
                // Update
                var updateCommand = new UpdateClientCommand
                {
                    Id = _view.SelectedClientId.Value,
                    FirstName = _view.FirstName,
                    LastName = _view.LastName,
                    PhoneNumber = _view.PhoneNumber,
                    Email = _view.Email,
                    Address = _view.Address,
                    City = _view.City,
                    State = _view.State,
                    ZipCode = _view.ZipCode
                };

                await _mediator.Send(updateCommand, ct);
                _view.ShowMessage("Cliente actualizado exitosamente", "Éxito", false);
            }
            else
            {
                // Create
                var createCommand = new CreateClientCommand
                {
                    FirstName = _view.FirstName,
                    LastName = _view.LastName,
                    PhoneNumber = _view.PhoneNumber,
                    Email = _view.Email,
                    Address = _view.Address,
                    City = _view.City,
                    State = _view.State,
                    ZipCode = _view.ZipCode
                };

                await _mediator.Send(createCommand, ct);
                _view.ShowMessage("Cliente creado exitosamente", "Éxito", false);
            }

            _view.ClearForm();
            await LoadClientsAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Guardado de cliente cancelado");
        }
        catch (DomainException ex)
        {
            var errorMessage = FormatValidationErrors(ex);
            _view.ShowMessage(errorMessage, "Error de Validación", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar cliente");
            _view.ShowMessage("Error al guardar cliente: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.EnableForm(true);
            _view.ShowLoading(false);
        }
    }

    private async void OnDeleteRequested(object? sender, EventArgs e)
    {
        if (!_view.SelectedClientId.HasValue)
        {
            _view.ShowMessage("Por favor seleccione un cliente para eliminar", "Advertencia", true);
            return;
        }

        var ct = GetCancellationToken();

        try
        {
            _view.EnableForm(false);
            _view.ShowLoading(true);

            var deleteCommand = new DeleteCommand<Data.Entities.Client>(_view.SelectedClientId.Value);
            await _mediator.Send(deleteCommand, ct);

            _view.ShowMessage("Cliente eliminado exitosamente", "Éxito", false);
            _view.ClearForm();
            await LoadClientsAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Eliminación de cliente cancelada");
        }
        catch (DomainException ex)
        {
            _view.ShowMessage(ex.Message, "Error", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar cliente");
            _view.ShowMessage("Error al eliminar cliente: " + ex.Message, "Error", true);
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
            await LoadClientsAsync();
            return;
        }

        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetAllQuery<Data.Entities.Client>
            {
                IgnorePagination = true  // Search needs all results to filter
            };
            var result = await _mediator.Send(query, ct);

            // Filtrar en el cliente (en producción sería mejor hacer esto en el backend)
            var searchText = _view.SearchText.ToLower();
            var filtered = result.Data.Items
                .Where(c =>
                    c.FirstName.ToLower().Contains(searchText) ||
                    c.LastName.ToLower().Contains(searchText) ||
                    c.City.ToLower().Contains(searchText) ||
                    c.PhoneNumber.Contains(searchText))
                .Select(c => new ClientViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FullName = $"{c.FirstName} {c.LastName}",
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email ?? string.Empty,
                    Address = c.Address,
                    City = c.City,
                    State = c.State,
                    ZipCode = c.ZipCode
                })
                .ToList();

            _view.LoadClients(filtered);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Búsqueda de clientes cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar clientes");
            _view.ShowMessage("Error al buscar: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.ShowLoading(false);
        }
    }

    private async void OnClientSelected(object? sender, int clientId)
    {
        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetByIdQuery<Data.Entities.Client>(clientId);
            var result = await _mediator.Send(query, ct);

            if (result.Data != null)
            {
                _view.SelectedClientId = result.Data.Id;
                _view.FirstName = result.Data.FirstName;
                _view.LastName = result.Data.LastName;
                _view.PhoneNumber = result.Data.PhoneNumber;
                _view.Email = result.Data.Email ?? string.Empty;
                _view.Address = result.Data.Address;
                _view.City = result.Data.City;
                _view.State = result.Data.State;
                _view.ZipCode = result.Data.ZipCode;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Carga de cliente cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar cliente");
            _view.ShowMessage("Error al cargar cliente: " + ex.Message, "Error", true);
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
            "FirstName" => "Nombre",
            "LastName" => "Apellido",
            "PhoneNumber" => "Teléfono",
            "Email" => "Email",
            "Address" => "Dirección",
            "City" => "Ciudad",
            "State" => "Estado",
            "ZipCode" => "Código Postal",
            _ => fieldName
        };
    }

    private async void OnPreviousPageRequested(object? sender, EventArgs e)
    {
        if (_currentPage > 1)
        {
            _currentPage--;
            await LoadClientsAsync();
        }
    }

    private async void OnNextPageRequested(object? sender, EventArgs e)
    {
        _currentPage++;
        await LoadClientsAsync();
    }
}
