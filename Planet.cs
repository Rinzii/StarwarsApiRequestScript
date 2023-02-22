using System.Text.Json.Serialization;

namespace StarwarsApiScript;

public class Planet
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("diameter")]
    public string Diameter { get; set; }

    [JsonPropertyName("climate")]
    public string Climate { get; set; }

    [JsonPropertyName("gravity")]
    public string Gravity { get; set; }

    [JsonPropertyName("population")]
    public string Population { get; set; }
}






