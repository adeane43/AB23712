using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AB23712.Context;

public static class ServiceCollectionExtensions
{
    public static void AddWeatherContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IWeatherForecastContext, WeatherForecastContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString(nameof(WeatherForecastContext))));
    }
}