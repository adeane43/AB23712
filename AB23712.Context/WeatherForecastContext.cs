using Microsoft.EntityFrameworkCore;

namespace AB23712_SignalR.Context;

public interface IWeatherForecastContext
{
    IEnumerable<WeatherForecast> GetForecasts(string contactId);
    IEnumerable<WeatherForecast> GetForecasts();
    void AddForecast(WeatherForecast weatherForecast);
    void UpdateForecast(WeatherForecast weatherForecast);
}

public class WeatherForecastContext : DbContext, IWeatherForecastContext
{
    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>(e =>
        {
            e.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();
        });
    }

    public IEnumerable<WeatherForecast> GetForecasts(string contactId)
        => WeatherForecasts
            .Where(x => x.ContactId == contactId);

    public IEnumerable<WeatherForecast> GetForecasts()
        => WeatherForecasts
            .ToArray();

    public void AddForecast(WeatherForecast weatherForecast)
    {
        WeatherForecasts.Add(weatherForecast);
        SaveChanges();
    }

    public void UpdateForecast(WeatherForecast weatherForecast)
    {
        WeatherForecasts.Update(weatherForecast);
        SaveChanges();
    }
}