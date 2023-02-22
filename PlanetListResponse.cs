using System.Text.Json.Serialization;

namespace StarwarsApiScript;

public class PlanetListResponse
{
    [JsonPropertyName("count")]
    public int? Count { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("previous")]
    public object Previous { get; set; }

    [JsonPropertyName("results")]
    public List<Planet> Planets { get; set; }
}