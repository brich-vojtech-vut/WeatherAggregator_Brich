namespace WeatherAggregator.Models;

public record WeatherData(
    string ProviderName, 
    string City, 
    double TemperatureCelsius, 
    bool IsSuccess, 
    string? ErrorMessage = null
);