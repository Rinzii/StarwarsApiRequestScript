using System.Text.Json;
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
    private readonly HttpClient _httpClient;
    private readonly IPlanetRepository _planetRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<PlanetService> _logger;

    public PlanetService(
        HttpClient httpClient, 
        IPlanetRepository planetRepository, 
        IMemoryCache memoryCache,
        ILogger<PlanetService> logger
        )
    {
        _httpClient = httpClient;
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
        var cacheKey = $"planet_{id}";
        if (_memoryCache.TryGetValue(cacheKey, out Planet? planet))
        {
            return planet;
        }

        try
        {
            planet = await _planetRepository.GetPlanetByIdAsync(id);
            _memoryCache.Set(cacheKey, planet, TimeSpan.FromMinutes(5));
            return planet;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get planet by ID: {id}");
            throw;
        }
    }

    /// <summary>
    /// Asynchronously gets a list of all the planets from the Star Wars API.
    /// </summary>
    /// <returns></returns>
    public async Task<List<Planet>?> GetAllPlanetsAsync()
    {
        var cacheKey = "planets";
        if (_memoryCache.TryGetValue(cacheKey, out List<Planet>? planets))
        {
            return planets;
        }

        try
        {
            planets = await _planetRepository.GetAllPlanetsAsync();
            _memoryCache.Set(cacheKey, planets, TimeSpan.FromMinutes(5));
            return planets;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get all planets");
            throw;
        }
    }
}