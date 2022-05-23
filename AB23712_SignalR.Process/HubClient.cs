using AB23712_SignalR.Context;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace AB23712_SignalR.Process;

public interface IHubClient
{
    Task SendForecast(WeatherForecast forecast);
}

public class HubClient : IHubClient
{
    private readonly HubConnection _connection;

    public HubClient(IConfiguration configuration)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl($"{configuration["AppBaseUrl"]}hub/connect")
            .Build();
    }

    public async Task SendForecast(WeatherForecast forecast)
    {
        if (_connection.State != HubConnectionState.Connected)
            await _connection.StartAsync();

        await _connection.SendAsync("ForecastChanged", forecast);
    }
}