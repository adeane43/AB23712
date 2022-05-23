using AB23712_SignalR.Context;
using Microsoft.AspNetCore.Mvc;

namespace AB23712_SignalR.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("{contactId}")]
    public IEnumerable<WeatherForecast> Get([FromRoute] string contactId)
        => _context.GetForecasts(contactId);

    [HttpGet("create/{contactId}")]
    public void Create([FromRoute] string contactId)
    {
        for (int i = 0; i < 10; i++)
            _context.AddForecast(new WeatherForecast
            {
                ContactId = contactId,
                Summary = $"test summary {i}",
                Date = DateTime.Now,
                TemperatureC = 0
            });
    }
}