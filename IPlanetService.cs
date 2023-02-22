namespace StarwarsApiScript;

public interface IPlanetService
{
    Task<Planet?> GetPlanetByIdAsync(int id);
    Task<List<Planet>?> GetAllPlanetsAsync();
}
