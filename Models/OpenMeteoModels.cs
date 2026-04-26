using System.Text.Json.Serialization;

namespace WeatherAggregator.Models;

public record OpenMeteoGeocodingResponse(
    [property: JsonPropertyName("results")] OpenMeteoLocation[]? Results
);

public record OpenMeteoLocation(
    [property: JsonPropertyName("latitude")] double Latitude,
    [property: JsonPropertyName("longitude")] double Longitude
);

public record OpenMeteoWeatherResponse(
    [property: JsonPropertyName("current_weather")] OpenMeteoCurrentWeather CurrentWeather
);

public record OpenMeteoCurrentWeather(
    [property: JsonPropertyName("temperature")] double Temperature
);