using Newtonsoft.Json;
using WorkerServiceAClient.DTO;

namespace WorkerServiceB;

public static class MessageHandler
{
    public static void HandleMessage(string message)
    {
        try
        {
            Console.WriteLine($"Received message: {message}");

            var weatherData = JsonConvert.DeserializeObject<WeatherKafkaMessage>(message);

            if (weatherData != null)
            {
                Console.WriteLine(
                    $"Weather for Kazan: {weatherData.LocalObservationDateTime + "  " + weatherData.WeatherText}," +
                    $" Temperature: {weatherData.Temperature} ");
                
                // fire and forget
                _ = GrpCsender.SendGrpcMessageAsync(weatherData);
            }
            else
            {
                Console.WriteLine("Deserialization resulted in null object.");
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
        }
    }
}