using System.Text.Json.Serialization;

namespace WeatherAggregator.Models;

public record WeatherApiResponse(
    [property: JsonPropertyName("current")] WeatherApiCurrent Current
);

public record WeatherApiCurrent(
    [property: JsonPropertyName("temp_c")] double TempC
);