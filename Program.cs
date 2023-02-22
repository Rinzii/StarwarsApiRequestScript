using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StarwarsApiScript;

class Program
{
    static async Task Main(string[] args)
    {
        await using var services = ConfigureServices();
        var planetService = services.GetRequiredService<IPlanetService>();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<Program>();
        
        try
        {
            // Get all of the planets using our planetService.
            var planets = await planetService.GetAllPlanetsAsync();
            
            // Write our list of planets to a CSV file.
            var csvWriter = new PlanetCsvWriter("planets.csv");
            csvWriter.Write(planets);
            
            // Log the number of planets we got.
            logger.LogInformation($"Number of planets: {planets!.Count}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all planets");
        }

        // Added to make it easier to know when the program has finished.
        // In a production environment this would be removed or replaced.
        Console.WriteLine("DONE!");
    }

    /// <summary>
    /// Configures the dependency injection container.
    /// </summary>
    /// <returns></returns>
    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddHttpClient();
        services.AddSingleton<IPlanetRepository, PlanetRepository>();
        services.AddSingleton<IPlanetService, PlanetService>();
        services.AddMemoryCache();
        services.AddLogging();

        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

        return services.BuildServiceProvider();
    }
}