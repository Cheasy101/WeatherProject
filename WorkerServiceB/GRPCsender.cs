using Grpc.Net.Client;
using GrpcServiceC;
using WorkerServiceAClient.DTO;
using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;

namespace WorkerServiceB;

public class GrpCsender
{
    private static readonly GrpcChannel Channel = GrpcChannel.ForAddress("https://localhost:5180");
    private static readonly WeatherService.WeatherServiceClient Client = new(Channel);

    public static async Task SendGrpcMessageAsync(WeatherKafkaMessage message)
    {
        var reply = await Client.SendWeatherInfoAsync(new WeatherRequest()
        {
            LocalObservationTime = Timestamp.FromDateTime(message.LocalObservationDateTime.ToUniversalTime()),
            WeatherText = message.WeatherText,
            Temperature = message.Temperature
        });
        Console.WriteLine("ReturnResponse: " + reply.Message);
    }
}