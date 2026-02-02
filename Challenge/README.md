# Dog Walking Business Management System

Sistema de gestión integral para negocios de paseo de perros, desarrollado con .NET 9.0 y Windows Forms, aplicando arquitectura limpia y patrones de diseño modernos.

## Tabla de Contenidos

- [Descripción](#descripción)
- [Características Principales](#características-principales)
- [Arquitectura y Patrones](#arquitectura-y-patrones)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Funcionalidades](#funcionalidades)
- [Base de Datos](#base-de-datos)
- [Uso del Sistema](#uso-del-sistema)
- [Patrones y Principios Aplicados](#patrones-y-principios-aplicados)
- [Comandos y Queries Genéricos](#comandos-y-queries-genéricos)

---

## Descripción

Aplicación de escritorio para gestionar operaciones de negocios de paseo de perros, permitiendo administrar:
- **Clientes**: Información completa de propietarios de mascotas
- **Perros**: Registro detallado de cada mascota con datos relevantes para el paseo
- **Paseos**: Registro de cada sesión de paseo con duración, distancia y notas
- **Autenticación**: Sistema de usuarios con contraseñas hasheadas (BCrypt)

## Características Principales

### Seguridad
- Autenticación de usuarios con contraseñas hasheadas usando BCrypt
- Sistema de sesión de usuario actual (ICurrentUserService)
- Validación de permisos por usuario

### Gestión de Datos
- **CRUD completo** para Clientes, Perros y Paseos
- **Paginación** de 20 registros por página en todas las listas
- **Búsqueda en tiempo real** por múltiples criterios
- **Validaciones robustas** con FluentValidation

### Interfaz de Usuario
- Diseño moderno con WinForms
- **MVP Pattern** (Model-View-Presenter) para separación de responsabilidades
- Feedback visual con estados de carga
- Mensajes de error amigables con traducción de campos
- Controles intuitivos (ComboBox, DateTimePicker, NumericUpDown)

### Auditoría
- **Temporal Tables** en SQL Server para historial completo de cambios
- Registro automático de fechas de creación y modificación
- Tracking de usuario que realizó cada paseo

### Rendimiento
- **CQRS** con contextos separados de lectura/escritura
- AsNoTracking en queries para optimización
- Paginación server-side
- CancellationToken en todas las operaciones asíncronas

---

## Arquitectura y Patrones

### Arquitectura en Capas

```
┌─────────────────────────────────────────────┐
│      Challenge.Presentation (UI)            │
│  - WinForms, Presenters, Views, ViewModels │
└────────────────┬────────────────────────────┘
                 │ MediatR Commands/Queries
┌────────────────▼────────────────────────────┐
│      Challenge.Business (Lógica)            │
│  - Handlers, Validators, Services, Mappers  │
│  - Acceso directo a DbContext               │
└────────────────┬────────────────────────────┘
                 │ EF Core
┌────────────────▼────────────────────────────┐
│      Challenge.Data (Acceso a Datos)        │
│  - WriteDbContext, ReadDbContext (CQRS)     │
│  - Configurations, Migrations               │
└────────────────┬────────────────────────────┘
                 │ SQL Server
┌────────────────▼────────────────────────────┐
│      Challenge.Models (Entidades)           │
│  - Entities, DTOs, Requests, Responses      │
└─────────────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────┐
│      Challenge.Core (Compartido)            │
│  - Interfaces, Exceptions, Result Pattern   │
└─────────────────────────────────────────────┘
```

### Patrones Implementados

1. **MVP (Model-View-Presenter)**
   - View: Solo renderiza UI y dispara eventos
   - Presenter: Contiene toda la lógica de negocio
   - Separación total entre UI y lógica

2. **CQRS (Command Query Responsibility Segregation)**
   - `WriteDbContext`: Para comandos (Create, Update, Delete) con tracking
   - `ReadDbContext`: Para queries (GetAll, GetById) con AsNoTracking
   - Handlers usan DbContext directamente (sin capa de repositorio)

3. **Mediator Pattern (MediatR)**
   - Desacoplamiento entre Presentation y Business
   - Pipeline behaviors para logging, validación y transacciones

4. **Result Pattern**
   - Manejo explícito de errores sin excepciones
   - Result<T> con Success/Failure
   - Error types: Validation, NotFound, Conflict, Internal

5. **Transaction Behavior**
   - Manejo automático de transacciones de base de datos
   - SaveChanges coordinado con commit/rollback
   - Aplicado solo a comandos que implementan ITransactionalCommand

---

## Tecnologías Utilizadas

### Framework y Lenguaje
- **.NET 9.0**
- **C# 13**
- **Windows Forms** (WinForms)

### Base de Datos
- **SQL Server**
- **Entity Framework Core 9.0**
- **Temporal Tables** (audit trail automático)

### Librerías Principales

| Librería | Versión | Propósito |
|----------|---------|-----------|
| MediatR | 12.4.0 | Mediator pattern, CQRS |
| FluentValidation | 11.9.0 | Validaciones declarativas |
| AutoMapper | 13.0.1 | Mapeo objeto-objeto |
| BCrypt.Net-Next | 4.0.3 | Hash de contraseñas |
| Microsoft.Extensions.DependencyInjection | 9.0.0 | Inyección de dependencias |
| Microsoft.Extensions.Logging | 9.0.0 | Logging |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.0 | ORM para SQL Server |
| Microsoft.EntityFrameworkCore.Tools | 9.0.0 | Migraciones |

---

## Requisitos Previos

- **Visual Studio 2022** (17.8 o superior) con:
  - Carga de trabajo ".NET Desktop Development"
  - Componente ".NET 9.0 Runtime"

- **.NET 9.0 SDK**
  - [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/9.0)

- **SQL Server**
  - Instalar [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)

---

## Instalación

### 1. Clonar el Repositorio

```bash
git clone <repository-url>
cd Challenge
```

### 2. Restaurar Paquetes NuGet

```bash
dotnet restore
```

O desde Visual Studio:
- Click derecho en la solución → "Restore NuGet Packages"

### 3. Configurar Connection String

Editar `Challenge.Presentation/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DogWalkingDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 4. Aplicar Migraciones

Desde Package Manager Console en Visual Studio:

```powershell
Update-Database
```

O desde línea de comandos:

```bash
cd Challenge.Data
dotnet ef database update --startup-project ..\Challenge.Presentation
```

### 5. Ejecutar la Aplicación

Presionar **F5** en Visual Studio o:

```bash
cd Challenge.Presentation
dotnet run
```

---

## Estructura del Proyecto

```
Challenge/
├── Challenge.Core/                 # Núcleo compartido
│   ├── Exceptions/
│   │   └── DomainException.cs      # Excepciones de dominio
│   ├── Interfaces/
│   │   ├── IIdentifier.cs          # Interfaz para entidades con ID
│   │   ├── ICurrentUserService.cs  # Servicio de usuario actual
│   │   └── ITransactionalCommand.cs # Marca comandos transaccionales
│   └── Common/
│       └── Result.cs               # Result Pattern
│
├── Challenge.Models/               # Modelos y DTOs
│   ├── Entities/
│   │   ├── Base/
│   │   │   └── BaseEntity.cs       # Entidad base con auditoría
│   │   ├── Person.cs               # Clase abstracta (TPH)
│   │   ├── Client.cs               # Cliente (hereda Person)
│   │   ├── User.cs                 # Usuario (hereda Person)
│   │   ├── Dog.cs                  # Perro
│   │   └── Walk.cs                 # Paseo
│   ├── Base/
│   │   ├── PagedRequest.cs         # Request paginado
│   │   └── PagedResponse.cs        # Response paginado
│   └── Enums/
│       └── PersonType.cs           # Enum: Client, User
│
├── Challenge.Data/                 # Capa de datos
│   ├── Context/
│   │   ├── WriteDbContext.cs       # Contexto de escritura (CQRS)
│   │   └── ReadDbContext.cs        # Contexto de lectura (CQRS)
│   ├── Configurations/
│   │   ├── PersonConfiguration.cs  # Config TPH + Temporal Tables
│   │   ├── ClientConfiguration.cs
│   │   ├── UserConfiguration.cs
│   │   ├── DogConfiguration.cs
│   │   └── WalkConfiguration.cs
│   ├── Extensions/
│   │   └── ServiceCollectionExtensions.cs # Registro de servicios de datos
│   └── Migrations/
│
├── Challenge.Business/             # Lógica de negocio
│   ├── Features/
│   │   ├── Generic/
│   │   │   ├── GetAll/             # Query genérico paginado
│   │   │   ├── GetById/            # Query genérico por ID
│   │   │   └── Delete/             # Command genérico delete
│   │   ├── Client/
│   │   │   ├── Create/
│   │   │   │   ├── CreateClientCommand.cs
│   │   │   │   └── CreateClientCommandHandler.cs
│   │   │   └── Update/
│   │   ├── Dog/
│   │   │   ├── Create/
│   │   │   └── Update/
│   │   └── Walk/
│   │       ├── Create/
│   │       └── Update/
│   ├── Validators/
│   │   ├── Client/
│   │   │   ├── CreateClientValidator.cs
│   │   │   └── UpdateClientValidator.cs
│   │   ├── Dog/
│   │   └── Walk/
│   ├── Behaviors/
│   │   ├── ValidationBehavior.cs   # Pipeline de validación
│   │   ├── LoggingBehavior.cs      # Pipeline de logging
│   │   └── TransactionBehavior.cs  # Pipeline transaccional
│   ├── Mappings/
│   │   └── ClientMappingProfile.cs # AutoMapper profiles
│   └── Services/
│       ├── AuthenticationService.cs
│       ├── PasswordHasher.cs
│       └── CurrentUserService.cs
│
└── Challenge.Presentation/         # Capa de presentación
    ├── Forms/
    │   ├── LoginForm.cs            # Autenticación
    │   ├── MainForm.cs             # Dashboard
    │   ├── ClientManagementForm.cs # Gestión de clientes
    │   ├── DogManagementForm.cs    # Gestión de perros
    │   └── WalkManagementForm.cs   # Gestión de paseos
    ├── Presenters/
    │   ├── Client/
    │   │   └── ClientPresenter.cs  # Lógica de ClientManagementForm
    │   ├── Dog/
    │   │   └── DogPresenter.cs
    │   └── Walk/
    │       └── WalkPresenter.cs
    ├── Views/
    │   ├── Client/
    │   │   └── IClientView.cs      # Contrato View-Presenter
    │   ├── Dog/
    │   │   └── IDogView.cs
    │   └── Walk/
    │       └── IWalkView.cs
    ├── ViewModels/
    │   ├── ClientViewModel.cs
    │   ├── DogViewModel.cs
    │   └── WalkViewModel.cs
    ├── Program.cs                  # Entry point + DI setup
    └── appsettings.json            # Configuración
```

---

## Funcionalidades

### 1. Autenticación de Usuarios

**LoginForm**

- Login con usuario y contraseña
- Validación de credenciales contra base de datos
- Contraseñas hasheadas con BCrypt
- Gestión de sesión de usuario actual
- Validación de cuenta bloqueada

**Credenciales por defecto:**
```
Usuario: admin
Contraseña: admin123
```

---

### 2. Gestión de Clientes

**ClientManagementForm**

#### Funcionalidades:
- **Crear cliente**
  - Campos: Nombre, Apellido, Teléfono, Email, Dirección, Ciudad, Estado, Código Postal
  - Validaciones: Campos requeridos, formato de email, formato de teléfono

- **Editar cliente**
  - Seleccionar cliente del grid
  - Modificar datos
  - Validación antes de guardar

- **Eliminar cliente**
  - Confirmación antes de eliminar
  - Validación de relaciones (no eliminar si tiene perros)

- **Buscar clientes**
  - Búsqueda por: Nombre, Apellido, Ciudad, Teléfono
  - Resultados en tiempo real

- **Listar clientes**
  - Paginación de 20 registros por página
  - Navegación: Anterior/Siguiente
  - Información: "Página X de Y (Total: Z registros)"

#### Validaciones:
```csharp
- FirstName: Requerido, máximo 100 caracteres
- LastName: Requerido, máximo 100 caracteres
- PhoneNumber: Requerido, formato internacional
- Email: Formato válido (opcional)
- Address: Requerido, máximo 200 caracteres
- City: Requerido, máximo 100 caracteres
- State: Requerido, máximo 100 caracteres
- ZipCode: Requerido, formato válido
```

---

### 3. Gestión de Perros

**DogManagementForm**

#### Funcionalidades:
- **Registrar perro**
  - Campos: Cliente (ComboBox), Nombre, Raza, Edad, Peso, Instrucciones Especiales
  - Validaciones: Cliente requerido, nombre requerido

- **Editar perro**
  - Seleccionar del grid
  - Modificar información

- **Eliminar perro**
  - Confirmación
  - Validación de paseos asociados

- **Buscar perros**
  - Por: Nombre del perro, Cliente, Raza

- **Listar perros**
  - Vista con cliente asociado
  - Paginación de 20 registros
  - Grid muestra: Perro, Cliente, Raza, Edad, Peso

#### Validaciones:
```csharp
- ClientId: Requerido
- Name: Requerido, máximo 100 caracteres
- Breed: Requerido, máximo 100 caracteres
- Age: Requerido, entre 0 y 30 años
- Weight: Opcional, entre 0.1 y 200 kg
- SpecialInstructions: Opcional, máximo 500 caracteres
```

---

### 4. Gestión de Paseos

**WalkManagementForm**

#### Funcionalidades:
- **Registrar paseo**
  - Campos: Perro (ComboBox), Fecha/Hora, Duración (minutos), Distancia (km), Notas
  - ComboBox muestra: "Nombre Perro (Cliente)"
  - Usuario paseador asignado automáticamente (usuario actual)

- **Editar paseo**
  - Modificar cualquier dato
  - Mantiene WalkedByUserId original

- **Eliminar paseo**
  - Con confirmación

- **Buscar paseos**
  - Por: Nombre del perro, Cliente, Notas

- **Listar paseos**
  - Ordenados por fecha descendente
  - Grid muestra: Perro, Cliente, Fecha, Duración, Distancia, Paseador
  - Paginación de 20 registros

#### Validaciones:
```csharp
- DogId: Requerido
- WalkDate: Requerido, no puede ser futuro
- DurationMinutes: Requerido, entre 5 y 1440 minutos
- Distance: Opcional, entre 0.1 y 100 km
- Notes: Opcional, máximo 500 caracteres
- WalkedByUserId: Requerido (auto-asignado)
```

#### Controles Especiales:
- **DateTimePicker**: Formato "dd/MM/yyyy HH:mm" con selector de hora
- **NumericUpDown**: Para duración y distancia con incrementos precisos
- **TextBox multilínea**: Para notas con scroll

---

## Base de Datos

### Modelo de Datos

```sql
Person (TPH - Table Per Hierarchy)
├── PersonId (PK, Identity)
├── FirstName (nvarchar(100))
├── LastName (nvarchar(100))
├── PhoneNumber (nvarchar(20))
├── Email (nvarchar(100), nullable)
├── PersonType (int) -- 1=Client, 2=User (Discriminator)
├── IsActive (bit)
├── CreatedAt (datetime2)
└── UpdatedAt (datetime2)

Client (hereda Person)
├── Address (nvarchar(200))
├── City (nvarchar(100))
├── State (nvarchar(100))
└── ZipCode (nvarchar(10))

User (hereda Person)
├── Username (nvarchar(50), unique)
├── PasswordHash (nvarchar(255))
├── Salt (nvarchar(255))
├── LastLoginDate (datetime2, nullable)
└── IsLocked (bit)

Dog
├── DogId (PK, Identity)
├── ClientId (FK → Person.PersonId)
├── Name (nvarchar(100))
├── Breed (nvarchar(100))
├── Age (int)
├── Weight (decimal(5,2), nullable)
├── SpecialInstructions (nvarchar(500), nullable)
├── IsActive (bit)
├── CreatedAt (datetime2)
└── UpdatedAt (datetime2)

Walk
├── WalkId (PK, Identity)
├── DogId (FK → Dog.DogId)
├── WalkDate (datetime2)
├── DurationMinutes (int)
├── Distance (decimal(5,2), nullable)
├── Notes (nvarchar(500), nullable)
├── WalkedBy (FK → Person.PersonId)
├── IsActive (bit)
├── CreatedAt (datetime2)
└── UpdatedAt (datetime2)
```

### Temporal Tables (Auditoría)

Todas las tablas tienen Temporal Tables habilitadas:

```sql
-- Historial automático en:
PersonHistory
DogHistory
WalkHistory

-- Columnas adicionales de sistema:
SysStartTime (datetime2) -- Inicio validez
SysEndTime (datetime2)   -- Fin validez
```

**Consultar historial:**

```sql
-- Ver todos los cambios de un perro
SELECT * FROM Dog
FOR SYSTEM_TIME ALL
WHERE DogId = 1;

-- Ver estado en fecha específica
SELECT * FROM Dog
FOR SYSTEM_TIME AS OF '2024-01-15 10:00:00'
WHERE DogId = 1;

-- Ver cambios en rango de fechas
SELECT * FROM Dog
FOR SYSTEM_TIME BETWEEN '2024-01-01' AND '2024-01-31'
WHERE DogId = 1;
```

### Índices

```sql
-- Índices automáticos por EF Core:
- PK en todas las entidades
- FK en relaciones

-- Índices recomendados (agregar si necesario):
CREATE INDEX IX_Dog_ClientId ON Dog(ClientId);
CREATE INDEX IX_Walk_DogId ON Walk(DogId);
CREATE INDEX IX_Walk_WalkDate ON Walk(WalkDate DESC);
CREATE INDEX IX_Person_Username ON Person(Username) WHERE PersonType = 2;
```

---

## Uso del Sistema

### Flujo de Trabajo Típico

#### 1. Login
```
1. Ejecutar aplicación
2. Ingresar usuario: admin
3. Ingresar contraseña: admin123
4. Click en "Iniciar Sesión"
5. → Se abre MainForm (Dashboard)
```

#### 2. Registrar un Cliente
```
1. Click en "Gestión de Clientes"
2. Completar formulario:
   - Nombre: Juan
   - Apellido: Pérez
   - Teléfono: +1234567890
   - Email: juan@example.com
   - Dirección: Calle 123
   - Ciudad: Santa Cruz
   - Estado: Santa Cruz
   - Código Postal: 00000
3. Click en "Guardar"
4. → Mensaje: "Cliente creado exitosamente"
5. → Cliente aparece en el grid
```

#### 3. Registrar un Perro
```
1. Click en "Gestión de Perros"
2. Seleccionar cliente del ComboBox
3. Completar:
   - Nombre: Max
   - Raza: Labrador
   - Edad: 3
   - Peso: 30.5
   - Instrucciones: Energético, necesita correr
4. Click en "Guardar"
5. → Perro registrado
```

#### 4. Registrar un Paseo
```
1. Click en "Gestión de Paseos"
2. Seleccionar perro: "Max (Juan Pérez)"
3. Seleccionar fecha/hora actual
4. Ingresar duración: 45 minutos
5. Ingresar distancia: 3.5 km
6. Agregar notas: "Paseo por el parque, muy activo"
7. Click en "Guardar"
8. → Paseo registrado (WalkedBy = usuario actual)
```

#### 5. Buscar Registros
```
1. En cualquier formulario de gestión
2. Ingresar término en "Búsqueda"
3. Click en "Buscar" o presionar Enter
4. → Grid filtra resultados
5. Click en "Actualizar" para ver todos
```

#### 6. Navegar Páginas
```
1. Ver info: "Página 1 de 5 (Total: 87 registros)"
2. Click en "Siguiente ▶" para página 2
3. Click en "◀ Anterior" para regresar
4. Botones se deshabilitan si no hay más páginas
```

#### 7. Editar un Registro
```
1. Click en fila del grid
2. → Datos se cargan en formulario
3. Modificar campos
4. Click en "Guardar"
5. → Mensaje: "XXX actualizado exitosamente"
```

#### 8. Eliminar un Registro
```
1. Click en fila del grid
2. Click en "Eliminar"
3. → Confirmación: "¿Está seguro...?"
4. Click en "Sí"
5. → Registro eliminado y grid actualizado
```

---

## Comandos y Queries Genéricos

El sistema incluye comandos y queries genéricos que reducen código repetitivo:

### GetAllQuery<TEntity>

Obtiene todos los registros de una entidad con paginación.

```csharp
// Uso básico
var query = new GetAllQuery<Client>
{
    PageSize = 20,
    PageNumber = 1
};
var result = await _mediator.Send(query);

// Con includes (carga eager de relaciones)
var query = new GetAllQuery<Dog>("Client")
{
    PageSize = 20,
    PageNumber = 1
};

// Sin paginación (para ComboBox)
var query = new GetAllQuery<Client>
{
    IgnorePagination = true
};
```

**Características:**
- Usa ReadDbContext (AsNoTracking)
- Filtra automáticamente por IsActive = true
- Retorna PagedResponse<T> con metadata de paginación
- Soporta includes para relaciones

### GetByIdQuery<TEntity>

Obtiene una entidad por su ID.

```csharp
var query = new GetByIdQuery<Client>(clientId);
var result = await _mediator.Send(query);

if (result.Data == null)
{
    // No encontrado
}
```

**Características:**
- Usa ReadDbContext (AsNoTracking)
- Retorna null si no existe
- No lanza excepción

### DeleteCommand<TEntity>

Elimina una entidad (hard delete).

```csharp
try
{
    var command = new DeleteCommand<Client>(clientId);
    var result = await _mediator.Send(command);

    if (result.IsSuccess)
    {
        MessageBox.Show("Eliminado exitosamente");
    }
}
catch (DomainException ex)
{
    // Manejo de error de dominio
}
```

**Características:**
- Usa WriteDbContext
- Hace hard delete (elimina físicamente)
- Requiere transacción (implementa ITransactionalCommand)
- Lanza DomainException si no existe

---

## Pipeline Behaviors (MediatR)

El sistema usa behaviors que se ejecutan antes/después de cada handler:

### 1. ValidationBehavior

Ejecuta automáticamente FluentValidators antes del handler.

```csharp
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(...)
    {
        // 1. Busca validators para TRequest
        // 2. Ejecuta validaciones
        // 3. Si falla, retorna Result.Failure con errores
        // 4. Si pasa, continúa al handler
    }
}
```

### 2. LoggingBehavior

Registra cada comando/query ejecutado.

```csharp
[LogInformation] Request: CreateClientCommand
[LogInformation] Response: Result<Success>
```

### 3. TransactionBehavior

Maneja transacciones automáticamente para comandos que implementan `ITransactionalCommand`.

```csharp
public async Task<TResponse> Handle(...)
{
    // Solo aplicar si implementa ITransactionalCommand
    if (request is not ITransactionalCommand)
    {
        return await next();
    }

    using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        var response = await next();
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return response;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}
```

**Características:**
- Reemplaza el patrón Unit of Work tradicional
- Más simple y directo
- Manejo automático de SaveChanges
- Rollback automático en caso de error

---

## Result Pattern

Manejo explícito de errores sin excepciones.

### Tipos de Errores

```csharp
public enum ErrorType
{
    Validation,  // Errores de validación
    NotFound,    // Recurso no encontrado
    Conflict,    // Conflicto de negocio
    Internal     // Error interno del servidor
}
```

### Uso de Result

```csharp
// Handler retorna Result
public async Task<Result<int>> Handle(CreateClientCommand request, ...)
{
    _logger.LogInformation("Creando cliente: {FirstName} {LastName}",
        request.FirstName, request.LastName);

    var client = _mapper.Map<Client>(request);
    client.PersonType = PersonType.Client;
    client.IsActive = true;

    await _context.Clients.AddAsync(client);

    // SaveChanges es manejado por TransactionBehavior

    return Result<int>.Success(client.Id);
}

// Presenter maneja Result
var result = await _mediator.Send(command);

if (result.IsSuccess)
{
    _view.ShowMessage("Cliente creado exitosamente", "Éxito", false);
}
else
{
    // Mostrar errores de validación formateados
    var errorMessage = FormatValidationErrors(result.Error);
    _view.ShowMessage(errorMessage, "Error", true);
}
```

---

## Seguridad

### Contraseñas

- Hasheadas con **BCrypt** (cost factor 12)
- Nunca se almacenan en texto plano
- Salt único por usuario

### Validaciones

- **FluentValidation** en capa de negocio
- Validaciones adicionales en UI
- Sanitización de inputs
