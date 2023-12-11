namespace WorkerServiceAClient.DTO;

public class WeatherKafkaMessage(DateTime localObservationDateTime, string? weatherText, string temperature)
{
    public DateTime LocalObservationDateTime { get; private set; } = localObservationDateTime;
    public string? WeatherText { get; private set; } = weatherText;
    public string Temperature { get; private set; } = temperature;
}