using System.Text;

namespace StarwarsApiScript;

public class PlanetCsvWriter
{
    private readonly string _filePath;

    /// <summary>
    /// Creates a new instance of the <see cref="PlanetCsvWriter"/> class.
    /// </summary>
    ///
    /// <param name="filePath">The path to the CSV file to write to.</param>
    public PlanetCsvWriter(string filePath)
    {
        _filePath = filePath;
    }

    /// <summary>
    /// Writes the given planets to a CSV file.
    /// </summary>
    ///
    /// <param name="planets">The planets to write to the CSV file.</param>
    public void Write(IEnumerable<Planet>? planets)
    {
        using (var streamWriter = new StreamWriter(_filePath, false, Encoding.UTF8))
        {
            // Write the header row
            streamWriter.WriteLine("\"Name\",\"Diameter\",\"Climate\",\"Gravity\",\"Population\"");

            // Write each planet as a row in the CSV file
            foreach (var planet in planets)
            {
                streamWriter.WriteLine($"\"{planet.Name}\",\"{planet.Diameter}\",\"{planet.Climate}\",\"{planet.Gravity}\",\"{planet.Population}\"");
            }
        }
    }
}
