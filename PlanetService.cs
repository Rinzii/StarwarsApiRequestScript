using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace StarwarsApiScript;

/// <summary>
/// Represents a service for interacting with the Star Wars API.
///
/// This service is responsible for caching the results of the API calls.
/// </summary>
public class PlanetService : IPlanetService
{
    private readonly IPlanetRepository _planetRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<PlanetService> _logger;

    public PlanetService(
        IPlanetRepository planetRepository, 
        IMemoryCache memoryCache,
        ILogger<PlanetService> logger
        )
    {
        _planetRepository = planetRepository;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    /// <summary>
    /// Asynchronously gets a planet by ID from the Star Wars API.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Planet?> GetPlanetByIdAsync(int id)
    {
        // Create a memory cache key for the planet with the given ID.
        var cacheKey = $"planet_{id}";
        if (_memoryCache.TryGetValue(cacheKey, out Planet? planet))
        {
            return planet;
        }

        // If the planet is not in the cache, get it from the Star Wars API.
        try
        {
            planet = await _planetRepository.GetPlanetByIdAsync(id);
            _memoryCache.Set(cacheKey, planet, TimeSpan.FromMinutes(5));
            return planet;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get planet by ID: {id} from Repository");
            throw;
        }
    }

    /// <summary>
    /// Asynchronously gets a list of all the planets from the Star Wars API.
    /// </summary>
    /// <returns></returns>
    public async Task<List<Planet>?> GetAllPlanetsAsync()
    {
        // Create a memory cache key for the list of planets.
        var cacheKey = "planets";
        if (_memoryCache.TryGetValue(cacheKey, out List<Planet>? planets))
        {
            return planets;
        }

        // If the list of planets is not in the cache, get it from the Star Wars API.
        try
        {
            planets = await _planetRepository.GetAllPlanetsAsync();
            _memoryCache.Set(cacheKey, planets, TimeSpan.FromMinutes(5));
            return planets;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get all planets from Repository");
            throw;
        }
    }
}