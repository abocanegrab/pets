# Comandos y Queries Genéricos

Este directorio contiene comandos y queries genéricos que pueden ser usados con cualquier entidad que implemente `IIdentifier`.

## Comandos/Queries Disponibles

### 1. DeleteCommand<TEntity> (Hard Delete)
Elimina permanentemente una entidad de la base de datos.

**Uso:**
```csharp
// En el controller/form
var command = new DeleteCommand<Client>(clientId);
var result = await _mediator.Send(command);
```

**Características:**
- Hace hard delete (elimina físicamente el registro)
- Requiere transacción (implementa ITransactionalCommand)
- Valida que el ID sea mayor que 0
- Lanza DomainException si no se encuentra la entidad

---

### 2. GetByIdQuery<TEntity>
Obtiene una entidad por su ID.

**Uso:**
```csharp
// En el controller/form
var query = new GetByIdQuery<Client>(clientId);
var result = await _mediator.Send(query);

if (result.Data == null)
{
    // Cliente no encontrado
}
```

**Características:**
- Usa ReadDbContext (sin tracking para mejor performance)
- Retorna null si no encuentra la entidad
- No lanza excepción si no existe

---

### 3. GetAllQuery<TEntity>
Obtiene todas las entidades de un tipo.

**Uso:**
```csharp
// En el controller/form
var query = new GetAllQuery<Client>();
var result = await _mediator.Send(query);

foreach (var client in result.Data)
{
    // Procesar cliente
}
```

**Características:**
- Usa ReadDbContext (sin tracking)
- Filtra automáticamente por `IsActive = true` si la entidad hereda de `BaseEntity`
- Retorna lista vacía si no hay entidades

---

## Ejemplo Completo de Uso

```csharp
public class ClientManagementForm : Form
{
    private readonly IMediator _mediator;

    public ClientManagementForm(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Listar todos los clientes
    private async Task LoadClientsAsync()
    {
        var query = new GetAllQuery<Client>();
        var result = await _mediator.Send(query);

        dgvClients.DataSource = result.Data;
    }

    // Obtener un cliente específico
    private async Task LoadClientAsync(int id)
    {
        var query = new GetByIdQuery<Client>(id);
        var result = await _mediator.Send(query);

        if (result.Data != null)
        {
            txtFirstName.Text = result.Data.FirstName;
            txtLastName.Text = result.Data.LastName;
            // ...
        }
    }

    // Eliminar cliente
    private async Task DeleteClientAsync(int id)
    {
        var command = new DeleteCommand<Client>(id);

        try
        {
            var result = await _mediator.Send(command);
            MessageBox.Show("Cliente eliminado exitosamente");
            await LoadClientsAsync();
        }
        catch (DomainException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
```

---

## Cuándo Usar Genéricos vs Específicos

### ✅ Usa Genéricos Para:
- Delete (hard delete simple)
- GetById (lectura simple por ID)
- GetAll (listar todos sin filtros complejos)

### ❌ Usa Específicos Para:
- **Create**: Requiere validaciones específicas por entidad
- **Update**: Lógica de negocio específica
- **Búsquedas complejas**: Con filtros, ordenamiento, paginación

---

## Nota sobre Hard Delete

Según los requisitos del proyecto, **DELETE hace hard delete** (elimina físicamente el registro).

Si en el futuro necesitas soft delete:
1. Cambia el handler genérico para usar `IsActive = false`
2. O crea un comando específico `SoftDeleteClientCommand`
