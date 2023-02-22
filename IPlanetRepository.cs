namespace StarwarsApiScript;

public interface IPlanetRepository
{
    Task<Planet?> GetPlanetByIdAsync(int id);
    Task<List<Planet>?> GetAllPlanetsAsync();
}

