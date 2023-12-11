using Confluent.Kafka;
using Newtonsoft.Json;
using WorkerServiceAClient.DTO;

namespace WorkerServiceA.Kafka;

public class KafkaProducerService
{
    private readonly ProducerConfig _producerConfig;

    public KafkaProducerService(string bootstrapServers)
    {
        _producerConfig = new ProducerConfig
        {
            BootstrapServers = bootstrapServers
        };
    }
        
    public async Task ProduceMessageAsync(string topic, WeatherKafkaMessage weatherData)
    {
        using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
        try
        {
            var serializedWeatherData = JsonConvert.SerializeObject(weatherData);

            var deliveryReport = await producer.ProduceAsync(topic, 
                new Message<Null, string> { Value = serializedWeatherData });

            Console.WriteLine($"Delivered message to {deliveryReport.TopicPartitionOffset}");
        }
        catch (ProduceException<Null, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
    }
        
}