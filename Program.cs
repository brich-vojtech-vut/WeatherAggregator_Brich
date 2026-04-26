using WeatherAggregator.Providers;
using WeatherAggregator.Services;

Console.WriteLine("=== Ultimátní porovnávací Appka pro počasí ===");
Console.WriteLine("                    ---                       ");

using var httpClient = new HttpClient();

string weatherApiKey = "5fb7bae12bc34b9392d82353262604"; // ZDE PATŘÍ KLÍČ Z WEATHERAPI.COM 

var providers = new List<IWeatherProvider>
{
    new OpenMeteoProvider(httpClient),
    
    new WeatherApiProvider(httpClient, weatherApiKey),
    
    new DummyApiProvider(httpClient) 
};

var aggregationService = new WeatherAggregationService(providers);

while (true)
{
    Console.Write("Zadejte název města (nebo 'q' pro ukončení): ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Vstup nemůže být prázdný.");
        continue;
    }

    if (input.Trim().ToLower() == "q")
    {
        Console.WriteLine("Ukončuji aplikaci...");
        break;
    }

    try
    {
        await aggregationService.ProcessWeatherForCityAsync(input);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Kritická chyba aplikace: {ex.Message}");
    }
}