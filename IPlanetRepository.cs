using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarwarsApiScript;

public interface IPlanetRepository
{
    Task<Planet?> GetPlanetByIdAsync(int id);
    Task<List<Planet>?> GetAllPlanetsAsync();
}

