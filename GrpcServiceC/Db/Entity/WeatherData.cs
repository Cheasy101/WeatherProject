namespace GrpcServiceC.Db.Entity;

public class WeatherData
{
    public int Id { get; set; }

    public DateTime LocalObservationDateTime { get; private set; }
    public string WeatherText { get; private set; }
    public string Temperature { get; private set; }

    public WeatherData(DateTime requestLocalObservationTime, string requestWeatherText, string requestTemperature)
    {
        Id = 0;
        LocalObservationDateTime = RoundToMinute(requestLocalObservationTime);
        WeatherText = requestWeatherText;
        Temperature = requestTemperature;
    }

    private DateTime RoundToMinute(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0,
            dateTime.Kind);
    }

    public WeatherData()
    {
    }
}