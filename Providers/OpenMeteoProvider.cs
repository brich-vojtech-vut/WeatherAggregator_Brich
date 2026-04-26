using System.Net.Http.Json;
using WeatherAggregator.Models;

namespace WeatherAggregator.Providers;

public class OpenMeteoProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;

    public OpenMeteoProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        try
        {
        var safeCityName = Uri.EscapeDataString(city);
        var geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={safeCityName}&count=1&language=en&format=json";
    
         var geoResponse = await _httpClient.GetFromJsonAsync<OpenMeteoGeocodingResponse>(geoUrl);

         if (geoResponse?.Results == null || geoResponse.Results.Length == 0)
         {
        return new WeatherData("OpenMeteo", city, 0, false, "Město nebylo nalezeno.");
        }

         var location = geoResponse.Results[0];
         var lat = location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
         var lon = location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

         var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
    
        var weatherResponse = await _httpClient.GetFromJsonAsync<OpenMeteoWeatherResponse>(weatherUrl);

         if (weatherResponse == null)
         {
        return new WeatherData("OpenMeteo", city, 0, false, "Nepodařilo se načíst data o počasí.");
         }

    return new WeatherData("OpenMeteo", city, weatherResponse.CurrentWeather.Temperature, true);
}
        catch (HttpRequestException ex)
        {
            return new WeatherData("OpenMeteo", city, 0, false, $"Chyba sítě: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new WeatherData("OpenMeteo", city, 0, false, $"Neočekávaná chyba: {ex.Message}");
        }
    }
}