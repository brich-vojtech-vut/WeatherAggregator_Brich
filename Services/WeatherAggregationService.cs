using WeatherAggregator.Models;
using WeatherAggregator.Providers;

namespace WeatherAggregator.Services;

public class WeatherAggregationService
{
    private readonly IEnumerable<IWeatherProvider> _providers;

    public WeatherAggregationService(IEnumerable<IWeatherProvider> providers)
    {
        _providers = providers;
    }

    public async Task ProcessWeatherForCityAsync(string city)
    {
        Console.WriteLine($"\nZahajuji asynchronní stahování dat pro město: {city}...");

        var tasks = _providers.Select(provider => provider.GetWeatherAsync(city)).ToList();
        
        var results = await Task.WhenAll(tasks); 

        var successfulResults = results.Where(r => r.IsSuccess).ToList();

        if (!successfulResults.Any())
        {
            Console.WriteLine("Nepodařilo se získat data z žádného zdroje.");
            return;
        }

        var maxTemp = successfulResults.Max(r => r.TemperatureCelsius);
        var minTemp = successfulResults.Min(r => r.TemperatureCelsius);
        var avgTemp = successfulResults.Average(r => r.TemperatureCelsius);

        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"Výsledky pro: {city}");
        foreach (var result in results)
        {
            if (result.IsSuccess)
                Console.WriteLine($"- {result.ProviderName}: {result.TemperatureCelsius}°C");
            else
                Console.WriteLine($"- {result.ProviderName}: CHYBA ({result.ErrorMessage})");
        }
        
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"Agregovaná data:");
        Console.WriteLine($"Průměrná teplota: {avgTemp:F1}°C");
        Console.WriteLine($"Nejvyšší teplota: {maxTemp}°C");
        Console.WriteLine($"Nejnižší teplota: {minTemp}°C");
        Console.WriteLine("------------------------------------------\n");
    }
}