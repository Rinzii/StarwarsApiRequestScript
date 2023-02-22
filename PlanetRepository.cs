using System.Text.Json;

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
    /// <exception cref="HttpRequestException"></exception>
    public async Task<Planet?> GetPlanetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://swapi.dev/api/planets/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to get planet with ID {id}.");
        }

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Planet>(json);
    }

    /// <summary>
    /// Asynchronously gets a list of all the planets from the Star Wars API.
    /// </summary>
    /// <returns>list of planets</returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<List<Planet>?> GetAllPlanetsAsync()
    {
        var planets = new List<Planet>();
        var nextUrl = "https://swapi.dev/api/planets/";

        while (!string.IsNullOrEmpty(nextUrl))
        {
            var response = await _httpClient.GetAsync(nextUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to get planets.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var planetList = JsonSerializer.Deserialize<PlanetListResponse>(json);

            planets.AddRange(planetList.Planets);
            nextUrl = planetList.Next;
        }

        return planets;
    }
}