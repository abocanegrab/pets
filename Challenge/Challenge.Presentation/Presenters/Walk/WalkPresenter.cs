using Challenge.Business.Features.Generic.Delete;
using Challenge.Business.Features.Generic.GetAll;
using Challenge.Business.Features.Generic.GetById;
using Challenge.Business.Features.Walk.Create;
using Challenge.Business.Features.Walk.Update;
using Challenge.Core.Exceptions;
using Challenge.Core.Interfaces;
using Challenge.Presentation.ViewModels;
using Challenge.Presentation.Views.Walk;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Challenge.Presentation.Presenters.Walk;

/// <summary>
/// Presenter que maneja toda la lógica de WalkManagementForm
/// La View solo notifica eventos, el Presenter decide qué hacer
/// </summary>
public class WalkPresenter : IDisposable
{
    private readonly IWalkView _view;
    private readonly IMediator _mediator;
    private readonly ILogger<WalkPresenter> _logger;
    private readonly ICurrentUserService _currentUserService;
    private CancellationTokenSource? _cts;
    private int _currentPage = 1;
    private const int PageSize = 20;

    public WalkPresenter(IWalkView view, IMediator mediator, ILogger<WalkPresenter> logger, ICurrentUserService currentUserService)
    {
        _view = view;
        _mediator = mediator;
        _logger = logger;
        _currentUserService = currentUserService;

        // Suscribirse a los eventos de la View
        _view.LoadRequested += OnLoadRequested;
        _view.SaveRequested += OnSaveRequested;
        _view.DeleteRequested += OnDeleteRequested;
        _view.ClearRequested += OnClearRequested;
        _view.SearchRequested += OnSearchRequested;
        _view.WalkSelected += OnWalkSelected;
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
        await LoadDogsAsync();
        await LoadWalksAsync();
    }

    private async void OnLoadRequested(object? sender, EventArgs e)
    {
        await LoadWalksAsync();
    }

    private async Task LoadWalksAsync()
    {
        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetAllQuery<Data.Entities.Walk>("Dog", "Dog.Client", "WalkedByUser")
            {
                PageSize = PageSize,
                PageNumber = _currentPage
            };
            var result = await _mediator.Send(query, ct);

            var viewModels = result.Data.Items.Select(w => new WalkViewModel
            {
                Id = w.Id,
                DogId = w.DogId,
                DogName = w.Dog?.Name ?? "N/A",
                ClientName = w.Dog?.Client != null ? $"{w.Dog.Client.FirstName} {w.Dog.Client.LastName}" : "N/A",
                WalkDate = w.WalkDate,
                DurationMinutes = w.DurationMinutes,
                Distance = w.Distance,
                Notes = w.Notes ?? string.Empty,
                WalkedByUsername = w.WalkedByUser?.Username ?? "N/A"
            }).OrderByDescending(w => w.WalkDate).ToList();

            _view.LoadWalks(viewModels);
            _view.UpdatePaginationInfo(
                result.Data.PageNumber,
                result.Data.TotalPages,
                result.Data.TotalCount,
                result.Data.HasPreviousPage,
                result.Data.HasNextPage);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Carga de paseos cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar paseos");
            _view.ShowMessage("Error al cargar paseos: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.ShowLoading(false);
        }
    }

    private async Task LoadDogsAsync()
    {
        var ct = GetCancellationToken();

        try
        {
            var query = new GetAllQuery<Data.Entities.Dog>("Client")
            {
                IgnorePagination = true  // Load all for ComboBox
            };
            var result = await _mediator.Send(query, ct);

            var dogs = result.Data.Items.Select(d => (
                d.Id,
                Name: d.Name,
                ClientName: d.Client != null ? $"{d.Client.FirstName} {d.Client.LastName}" : "N/A"
            )).ToList();

            _view.LoadDogs(dogs);
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
    }

    private async void OnSaveRequested(object? sender, EventArgs e)
    {
        var ct = GetCancellationToken();

        try
        {
            _view.EnableForm(false);
            _view.ShowLoading(true);

            // Validaciones básicas antes de enviar el comando
            if (!_view.SelectedDogId.HasValue)
            {
                _view.ShowMessage("Por favor seleccione un perro", "Validación", true);
                return;
            }

            if (!int.TryParse(_view.DurationMinutes, out int duration))
            {
                _view.ShowMessage("Por favor ingrese una duración válida en minutos", "Validación", true);
                return;
            }

            if (!decimal.TryParse(_view.Distance, out decimal distance))
            {
                _view.ShowMessage("Por favor ingrese una distancia válida", "Validación", true);
                return;
            }

            if (_view.SelectedWalkId.HasValue)
            {
                // Update
                var updateCommand = new UpdateWalkCommand
                {
                    Id = _view.SelectedWalkId.Value,
                    DogId = _view.SelectedDogId.Value,
                    WalkDate = _view.WalkDate,
                    DurationMinutes = duration,
                    Distance = distance,
                    Notes = _view.Notes,
                    WalkedByUserId = _currentUserService.UserId
                };

                await _mediator.Send(updateCommand, ct);
                _view.ShowMessage("Paseo actualizado exitosamente", "Éxito", false);
            }
            else
            {
                // Create
                var createCommand = new CreateWalkCommand
                {
                    DogId = _view.SelectedDogId.Value,
                    WalkDate = _view.WalkDate,
                    DurationMinutes = duration,
                    Distance = distance,
                    Notes = _view.Notes,
                    WalkedByUserId = _currentUserService.UserId
                };

                await _mediator.Send(createCommand, ct);
                _view.ShowMessage("Paseo registrado exitosamente", "Éxito", false);
            }

            _view.ClearForm();
            await LoadWalksAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Guardado de paseo cancelado");
        }
        catch (DomainException ex)
        {
            var errorMessage = FormatValidationErrors(ex);
            _view.ShowMessage(errorMessage, "Error de Validación", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar paseo");
            _view.ShowMessage("Error al guardar paseo: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.EnableForm(true);
            _view.ShowLoading(false);
        }
    }

    private async void OnDeleteRequested(object? sender, EventArgs e)
    {
        if (!_view.SelectedWalkId.HasValue)
        {
            _view.ShowMessage("Por favor seleccione un paseo para eliminar", "Advertencia", true);
            return;
        }

        var ct = GetCancellationToken();

        try
        {
            _view.EnableForm(false);
            _view.ShowLoading(true);

            var deleteCommand = new DeleteCommand<Data.Entities.Walk>(_view.SelectedWalkId.Value);
            await _mediator.Send(deleteCommand, ct);

            _view.ShowMessage("Paseo eliminado exitosamente", "Éxito", false);
            _view.ClearForm();
            await LoadWalksAsync();
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Eliminación de paseo cancelada");
        }
        catch (DomainException ex)
        {
            _view.ShowMessage(ex.Message, "Error", true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar paseo");
            _view.ShowMessage("Error al eliminar paseo: " + ex.Message, "Error", true);
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
            await LoadWalksAsync();
            return;
        }

        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetAllQuery<Data.Entities.Walk>("Dog", "Dog.Client", "WalkedByUser")
            {
                IgnorePagination = true  // Search needs all results to filter
            };
            var result = await _mediator.Send(query, ct);

            // Filtrar en el cliente
            var searchText = _view.SearchText.ToLower();
            var filtered = result.Data.Items
                .Where(w =>
                    (w.Dog != null && w.Dog.Name.ToLower().Contains(searchText)) ||
                    (w.Dog?.Client != null &&
                     (w.Dog.Client.FirstName.ToLower().Contains(searchText) ||
                      w.Dog.Client.LastName.ToLower().Contains(searchText))) ||
                    (w.Notes != null && w.Notes.ToLower().Contains(searchText)))
                .Select(w => new WalkViewModel
                {
                    Id = w.Id,
                    DogId = w.DogId,
                    DogName = w.Dog?.Name ?? "N/A",
                    ClientName = w.Dog?.Client != null ? $"{w.Dog.Client.FirstName} {w.Dog.Client.LastName}" : "N/A",
                    WalkDate = w.WalkDate,
                    DurationMinutes = w.DurationMinutes,
                    Distance = w.Distance,
                    Notes = w.Notes ?? string.Empty,
                    WalkedByUsername = w.WalkedByUser?.Username ?? "N/A"
                })
                .OrderByDescending(w => w.WalkDate)
                .ToList();

            _view.LoadWalks(filtered);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Búsqueda de paseos cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar paseos");
            _view.ShowMessage("Error al buscar: " + ex.Message, "Error", true);
        }
        finally
        {
            _view.ShowLoading(false);
        }
    }

    private async void OnWalkSelected(object? sender, int walkId)
    {
        var ct = GetCancellationToken();

        try
        {
            _view.ShowLoading(true);

            var query = new GetByIdQuery<Data.Entities.Walk>(walkId);
            var result = await _mediator.Send(query, ct);

            if (result.Data != null)
            {
                _view.SelectedWalkId = result.Data.Id;
                _view.SelectedDogId = result.Data.DogId;
                _view.WalkDate = result.Data.WalkDate;
                _view.DurationMinutes = result.Data.DurationMinutes.ToString();
                _view.Distance = result.Data.Distance.ToString();
                _view.Notes = result.Data.Notes ?? string.Empty;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Carga de paseo cancelada");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar paseo");
            _view.ShowMessage("Error al cargar paseo: " + ex.Message, "Error", true);
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
            "DogId" => "Perro",
            "WalkDate" => "Fecha del Paseo",
            "DurationMinutes" => "Duración (minutos)",
            "Distance" => "Distancia (km)",
            "Notes" => "Notas",
            _ => fieldName
        };
    }

    private async void OnPreviousPageRequested(object? sender, EventArgs e)
    {
        if (_currentPage > 1)
        {
            _currentPage--;
            await LoadWalksAsync();
        }
    }

    private async void OnNextPageRequested(object? sender, EventArgs e)
    {
        _currentPage++;
        await LoadWalksAsync();
    }
}
