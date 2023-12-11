using GrpcServiceC.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceC.Db;

public sealed class WeatherDbContext : DbContext
{
    public DbSet<WeatherData> WeatherData { get; set; }


    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherData>()
            .HasIndex(w => w.LocalObservationDateTime)
            .IsUnique();
    }
}