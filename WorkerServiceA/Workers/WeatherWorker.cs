using WorkerServiceA.WeatherController;
using WorkerServiceA.Kafka;

namespace WorkerServiceA
{
    public class WeatherWorker : BackgroundService
    {
        private readonly ILogger<WeatherWorker> _logger;
        private readonly WeatherWorkerConfiguration _configuration;
        private readonly KafkaProducerService _kafkaProducer;

        public WeatherWorker(ILogger<WeatherWorker> logger, WeatherWorkerConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _kafkaProducer = new KafkaProducerService(_configuration.KafkaBootstrapServers);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var weatherResponse = await _configuration.AccuWeatherService.GetWeatherForCityAsync(_configuration.CityKey);

                    if (weatherResponse != null)
                    {
                        var weatherKafkaMessage = DtoToKafkaConverter.ConvertFromApiToKafkaMessage(weatherResponse);
                        _logger.LogInformation(
                            $"Weather for Kazan: {weatherKafkaMessage.LocalObservationDateTime + "  " + weatherKafkaMessage.WeatherText}," +
                            $" Temperature: {weatherKafkaMessage.Temperature} ");

                        await _kafkaProducer.ProduceMessageAsync(_configuration.KafkaTopic, weatherKafkaMessage);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error during weather data retrieval: {ex.Message}");
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

    public class WeatherWorkerConfiguration(AccuWeatherService accuWeatherService)
    {
        public AccuWeatherService AccuWeatherService { get; } = accuWeatherService;
        public string KafkaBootstrapServers { get; } = "localhost:9092";
        public string KafkaTopic { get; } = "weatherTopic";
        public string CityKey { get; } = "295954"; // Казанский ключ
    }
}
