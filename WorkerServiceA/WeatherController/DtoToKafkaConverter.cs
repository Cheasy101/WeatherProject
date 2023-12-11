using WorkerServiceAClient.DTO;

namespace WorkerServiceA.WeatherController;

public static class DtoToKafkaConverter
{
    
    public static WeatherKafkaMessage ConvertFromApiToKafkaMessage(WeatherResponseDTO apiResponse)
    {
        var localObservationDateTime = DateTime.UtcNow.AddHours(3); 
        var weatherText = apiResponse.WeatherText;
        var temperatureValue = apiResponse.Temperature.Metric.Value;
        var temperatureUnit = apiResponse.Temperature.Metric.Unit;
        var temperature = $"{temperatureValue} {temperatureUnit}";

        return new WeatherKafkaMessage(localObservationDateTime, weatherText, temperature);
    }

}