using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Bridge.Client;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetupConfiguration(args)
            .Build();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services => services.SetupServices(configuration))
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        var startup = ActivatorUtilities.CreateInstance<Startup>(host.Services);
        await startup.Run();

        Console.ReadLine();
    }

    private static IConfigurationBuilder SetupConfiguration(
        this IConfigurationBuilder builder, string[] args)
    {
        return builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddCommandLine(args)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    }

    private static IServiceCollection SetupServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<StartupOptions>(configuration.GetSection("Startup"));
    }
}
