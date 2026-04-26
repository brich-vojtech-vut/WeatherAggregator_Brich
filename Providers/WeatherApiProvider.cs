using System.Net.Http.Json;
using WeatherAggregator.Models;

namespace WeatherAggregator.Providers;

public class WeatherApiProvider : IWeatherProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherApiProvider(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        try
        {
            var safeCityName = Uri.EscapeDataString(city);
            
            var url = $"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={safeCityName}&aqi=no";

            var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(url);

            if (response == null)
            {
                return new WeatherData("WeatherAPI", city, 0, false, "Nepodařilo se načíst data.");
            }

            return new WeatherData("WeatherAPI", city, response.Current.TempC, true);
        }
        catch (HttpRequestException ex)
        {
            // WeatherAPI vrací chybu 400, když město neexistuje, a 401/403 pro špatný klíč
            if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return new WeatherData("WeatherAPI", city, 0, false, "Město nebylo nalezeno.");
            }
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized || ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return new WeatherData("WeatherAPI", city, 0, false, "Neplatný API klíč.");
            }

            return new WeatherData("WeatherAPI", city, 0, false, $"Chyba sítě: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new WeatherData("WeatherAPI", city, 0, false, $"Neočekávaná chyba: {ex.Message}");
        }
    }
}