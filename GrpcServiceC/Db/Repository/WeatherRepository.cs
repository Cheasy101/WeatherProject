using GrpcServiceC.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceC.Db.Repository;

public class WeatherRepository(WeatherDbContext weatherDbContext) : DbContext
{
    public void SaveToDb(WeatherData weatherData)
    {
        weatherDbContext.WeatherData.Add(weatherData);
        weatherDbContext.SaveChanges();
    }

    public async Task<List<WeatherData>> GetLastTenRecordsAsync()
    {
        IQueryable<WeatherData> query = weatherDbContext.WeatherData;

        List<WeatherData> lastTenRecords = await query
            .OrderByDescending(time => time.LocalObservationDateTime)
            .Take(10)
            .ToListAsync();

        return lastTenRecords;
    }
}