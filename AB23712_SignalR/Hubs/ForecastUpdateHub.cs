using AB23712_SignalR.Context;
using Microsoft.AspNetCore.SignalR;

namespace AB23712_SignalR.Hubs;

public class ForecastUpdateHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task ForecastChanged(WeatherForecast forecast)
    {
        await Clients
            .Group(forecast.ContactId)
            .SendAsync(nameof(ForecastChanged), forecast);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        
        string contactId = httpContext.Request.Query["contactId"];

        if (contactId is null)
            return;

        await Groups.AddToGroupAsync(Context.ConnectionId, contactId);

        await base.OnConnectedAsync();
    }
}