namespace WorkerServiceA.WeatherController;

public class WeatherResponseDTO
{
    public string? WeatherText { get; set; }
    public Temperature? Temperature { get; set; }
}

public class Temperature
{
    public MetricMetric? Metric { get; set; }
}

public class MetricMetric
{
    public double Value { get; set; }
    public string? Unit { get; set; }
}