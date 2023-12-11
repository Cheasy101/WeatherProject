using WorkerServiceA;
using WorkerServiceA.WeatherController;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<WeatherWorkerConfiguration>();
builder.Services.AddHostedService<WeatherWorker>();
builder.Services.AddSingleton<AccuWeatherService>(_ =>
    new AccuWeatherService(Environment.GetEnvironmentVariable("ACCU_WEATHER_API_KEY")));
var host = builder.Build();
host.Run();