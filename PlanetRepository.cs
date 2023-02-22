using System.Net.Http.Json;
using CleGuards2023SoftDev.Exceptions;

namespace StarwarsApiScript;

/// <summary>
/// The PlanetRepository class is responsible for making HTTP requests to the Star Wars API.
///
/// This is a concrete implementation of the IPlanetRepository interface using the Repository pattern.
/// </summary>
public class PlanetRepository : IPlanetRepository
{
    private readonly HttpClient _httpClient;

    public PlanetRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Asynchronously gets a planet by ID from the Star Wars API.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ApiException"></exception>
    public async Task<Planet?> GetPlanetByIdAsync(int id)
    {
        var planet = await _httpClient.GetFromJsonAsync<Planet>($"https://swapi.dev/api/planets/{id}");

        if (planet == null)
        {
            throw new ApiException($"Failed to get planet with ID {id} from API.");
        }

        return planet;
    }

    /// <summary>
    /// Asynchronously gets a list of all the planets from the Star Wars API.
    /// </summary>
    /// <returns>list of planets</returns>
    /// <exception cref="ApiException"></exception>
    public async Task<List<Planet>?> GetAllPlanetsAsync()
    {
        var planets = new List<Planet>();
        var nextUrl = "https://swapi.dev/api/planets/";

        while (!string.IsNullOrEmpty(nextUrl))
        {
            var planetList = await _httpClient.GetFromJsonAsync<PlanetListResponse>(nextUrl);
            
            if (planetList == null)
            {
                throw new ApiException($"Failed to get planet list from API.");
            }

            planets.AddRange(planetList.Planets);
            nextUrl = planetList.Next;
        }

        return planets;
    }
}