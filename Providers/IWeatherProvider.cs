using WeatherAggregator.Models;

namespace WeatherAggregator.Providers;

public interface IWeatherProvider
{
    Task<WeatherData> GetWeatherAsync(string city);
}