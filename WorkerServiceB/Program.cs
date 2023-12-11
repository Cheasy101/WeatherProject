using WorkerServiceB;
using WorkerServiceB.KafkaConsumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<ILogger<KafkaReader>, Logger<KafkaReader>>();
builder.Services.AddHostedService<KafkaReceiverWorker>();
builder.Services.AddSingleton<KafkaReader>();

var host = builder.Build();
host.Run();