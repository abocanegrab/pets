using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Challenge.Data.Context;

/// <summary>
/// Factory para crear WriteDbContext en tiempo de diseño (migraciones)
/// </summary>
public class WriteDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
{
    public WriteDbContext CreateDbContext(string[] args)
    {
        // Buscar appsettings.json en el proyecto Presentation (startup project)
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Challenge.Presentation"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false)
            .Build();

        var connectionString = configuration.GetConnectionString("MigrationConnection")
            ?? throw new InvalidOperationException("MigrationConnection not found in appsettings.json");

        var optionsBuilder = new DbContextOptionsBuilder<WriteDbContext>();
        optionsBuilder.UseSqlServer(
            connectionString,
            b => b.MigrationsAssembly("Challenge.Data"));

        // currentUserService = null porque no hay usuario autenticado en tiempo de diseño (solo operaciones del sistema)
        return new WriteDbContext(optionsBuilder.Options, currentUserService: null);
    }
}

