using AB23712_SignalR.Context;

namespace AB23712_SignalR.Process;

public interface IForecastService
{
    public Task UpdateForecast(WeatherForecast forecast);
    public IEnumerable<WeatherForecast> GetForecasts();
}

public class ForecastService : IForecastService
{
    private readonly IWeatherForecastContext _context;
    private readonly IHubClient _hubClient;

    public ForecastService(IWeatherForecastContext context, IHubClient hubClient)
    {
        _context = context;
        _hubClient = hubClient;
    }

    public async Task UpdateForecast(WeatherForecast forecast)
    {
        await _hubClient.SendForecast(forecast);
        _context.UpdateForecast(forecast);
    }

    public IEnumerable<WeatherForecast> GetForecasts() => _context.GetForecasts();
}