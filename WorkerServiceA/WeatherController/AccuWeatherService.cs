using Newtonsoft.Json;

namespace WorkerServiceA.WeatherController;

public class AccuWeatherService
{
    private readonly string? apiKey;
    private readonly HttpClient httpClient;

    public AccuWeatherService(string? apiKey)
    {
        this.apiKey = apiKey;
        httpClient = new HttpClient();
    }


    public async Task<WeatherResponseDTO?> GetWeatherForCityAsync(string cityKey)

    {
        var apiUrl =
            $"http://dataservice.accuweather.com/currentconditions/v1/{cityKey}?apikey={apiKey}";

        var response = await httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<WeatherResponseDTO[]>(content);

            return weatherData?.FirstOrDefault();
        }

        Console.WriteLine($"Error: {response.StatusCode}");
        return null;
    }
}