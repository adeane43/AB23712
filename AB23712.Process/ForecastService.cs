using AB23712.Context;

namespace AB23712.Process;

public interface IForecastService
{
    public void UpdateForecast(WeatherForecast forecast);
    public IEnumerable<WeatherForecast> GetForecasts();
}

public class ForecastService : IForecastService
{
    private readonly IWeatherForecastContext _context;

    public ForecastService(IWeatherForecastContext context)
    {
        _context = context;
    }

    public void UpdateForecast(WeatherForecast forecast)
    {
        _context.UpdateForecast(forecast);
    }

    public IEnumerable<WeatherForecast> GetForecasts() => _context.GetForecasts();
}