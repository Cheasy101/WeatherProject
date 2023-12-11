using Newtonsoft.Json;
using WorkerServiceAClient.DTO;
using WorkerServiceB.KafkaConsumer;
using JsonException = System.Text.Json.JsonException;

namespace WorkerServiceB;

public class KafkaReceiverWorker(ILogger<KafkaReceiverWorker> logger, KafkaReader kafkaReader)
    : BackgroundService
{
    
     

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await kafkaReader.StartReadingAsync(stoppingToken, MessageHandler.HandleMessage);

            Console.WriteLine("Stopping the application");
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in worker: {ex.Message}");
        }

        await Task.Delay(1, stoppingToken);
    }
}