using AB23712_SignalR.Context;
using Microsoft.Azure.WebJobs;

namespace AB23712_SignalR.Process;

public class Functions
{
    private readonly IForecastService _forecastService;
    private static readonly int NumStatuses = Enum.GetValues<Status>().Length;

    public Functions(IForecastService forecastService)
    {
        _forecastService = forecastService;
    }

    public async Task CycleTemperatures(
        [TimerTrigger(scheduleExpression: "* * * * * *", RunOnStartup = true)]
        TimerInfo timerInfo)
    {
        var forecasts = _forecastService.GetForecasts();

        if (!forecasts.Any())
            return;

        var groupedByUser = forecasts.GroupBy(x => x.ContactId);

        var random = new Random();

        // Change and update one of them for each user
        foreach(var group in groupedByUser)
        {
            var forecast = group.ElementAt(random.Next(0, group.Count()));

            forecast.Date = DateTime.Now;
            forecast.TemperatureC = random.Next(0, 100);

            // Increment the status
            forecast.Status = (Status)((int)(forecast.Status + 1) % NumStatuses);

            await _forecastService.UpdateForecast(forecast);
        }
    }
}