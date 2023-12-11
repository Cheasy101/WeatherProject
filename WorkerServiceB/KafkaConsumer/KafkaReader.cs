using Confluent.Kafka;

namespace WorkerServiceB.KafkaConsumer;

public class KafkaReader : IDisposable
{
    private readonly ConsumerConfig _consumerConfig;
    private readonly ILogger<KafkaReader> _logger;
    private readonly IConsumer<Ignore, string> _consumer;

    public KafkaReader(ILogger<KafkaReader> logger)
    {
        _logger = logger;

        _consumerConfig = new ConsumerConfig
        {
            GroupId = "my-consumer-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        _consumer.Subscribe("weatherTopic");
    }

    public async Task StartReadingAsync(CancellationToken stoppingToken, Func<string, Task> messageHandler)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                if (result.Message != null)
                {
                    await messageHandler(result.Message.Value);
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Reading from Kafka canceled.");
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Error consuming message: {e.Error.Reason}");
            }
        }
    }

    public void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
    }
}