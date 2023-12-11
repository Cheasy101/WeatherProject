using Grpc.Core;
using GrpcServiceC.Db.Entity;
using GrpcServiceC.Db.Repository;

namespace GrpcServiceC.Services;

public class WeatherService(WeatherRepository weatherRepository) : GrpcServiceC.WeatherService.WeatherServiceBase
{

    public override Task<WeatherReply> SendWeatherInfo(WeatherRequest request, ServerCallContext context)
    {
        Console.WriteLine(request.LocalObservationTime);
        Console.WriteLine(request.Temperature);
        Console.WriteLine(request.WeatherText);
        // тут будем вызывать объект и подрубаться к бд
        var weatherData =
            new WeatherData(request.LocalObservationTime.ToDateTime(), request.WeatherText, request.Temperature);
        
        weatherRepository.SaveToDb(weatherData);
        
        
        return Task.FromResult(new WeatherReply(){Message = "Все ок "});
    }
}