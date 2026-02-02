using Challenge.Core.Interfaces;
using Challenge.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Data.Context;

/// <summary>
/// DbContext para operaciones de escritura (Commands - Create, Update, Delete)
/// Con tracking habilitado y auditoría automática
/// </summary>
public class WriteDbContext : DbContext
{
    private readonly int _currentUserId;

    public WriteDbContext(DbContextOptions<WriteDbContext> options, int currentUserId = 0)
        : base(options)
    {
        _currentUserId = currentUserId;
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Walk> Walks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar todas las configuraciones del assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        // Manejar entidades creadas
        var createdEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added && e.Entity is ICreated);

        foreach (var entry in createdEntries)
        {
            var entity = (ICreated)entry.Entity;

            // Solo establecer si no fue establecido manualmente (ej: seed data)
            if (entity.CreatedAt == default)
                entity.CreatedAt = now;

            if (entity.CreatedBy == 0)
                entity.CreatedBy = _currentUserId;

            if (entity is IModified modifiedEntity)
            {
                if (modifiedEntity.UpdatedAt == default)
                    modifiedEntity.UpdatedAt = now;

                if (modifiedEntity.UpdatedBy == 0)
                    modifiedEntity.UpdatedBy = _currentUserId;
            }
        }

        // Manejar entidades modificadas
        var modifiedEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified && e.Entity is IModified);

        foreach (var entry in modifiedEntries)
        {
            var entity = (IModified)entry.Entity;

            // Siempre actualizar la fecha/usuario de modificación para cambios reales
            entity.UpdatedAt = now;
            if (_currentUserId != 0)
                entity.UpdatedBy = _currentUserId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
