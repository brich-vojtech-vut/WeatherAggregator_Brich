using System.Text.Json;
using WeatherAggregator.Models;

namespace WeatherAggregator.Providers;

public class DummyApiProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;

    public DummyApiProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        try
        {
            await Task.Delay(500); 
            var randomTemp = new Random().Next(5, 25); 
            
            return new WeatherData("DummyAPI", city, randomTemp, true);
        }
        catch (HttpRequestException ex)
        {
            return new WeatherData("DummyAPI", city, 0, false, $"Chyba sítě: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new WeatherData("DummyAPI", city, 0, false, $"Neočekávaná chyba: {ex.Message}");
        }
    }
}