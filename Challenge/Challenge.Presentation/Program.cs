using Challenge.Business.Extensions;
using Challenge.Data.Extensions;
using Challenge.Presentation.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Challenge.Presentation;

internal static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Crear y configurar el host con Dependency Injection
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

        // Ejecutar la aplicación con LoginForm como punto de entrada
        Application.Run(ServiceProvider.GetRequiredService<LoginForm>());
    }

    static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Configurar logging
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                });

                // Registrar capas de la aplicación
                services.AddDataServices(context.Configuration);
                services.AddBusinessServices();

                // Registrar Forms como Transient (nueva instancia cada vez)
                services.AddTransient<LoginForm>();
                services.AddTransient<MainForm>();
                services.AddTransient<ClientManagementForm>();
                services.AddTransient<DogManagementForm>();
                services.AddTransient<WalkManagementForm>();
            });
    }
}
