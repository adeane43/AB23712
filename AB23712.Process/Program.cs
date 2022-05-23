using AB23712_SignalR.Context;
using AB23712_SignalR.Process;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Create WebJob host
var builder = new HostBuilder();

// DI services
builder.ConfigureServices(((context, services) =>
{
    services.AddWeatherContext(context.Configuration);
    services.AddScoped<IForecastService, ForecastService>();
    services.AddSingleton<IHubClient, HubClient>();
    services.AddSignalR();
}));

builder.ConfigureWebJobs(b =>
{
    b.AddAzureStorageCoreServices();
    b.AddTimers();
});

builder.ConfigureLogging((context, loggingBuilder) =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddFilter("AB23712_SignalR", LogLevel.Debug);
    loggingBuilder.AddFilter(LogCategories.HostGeneral, LogLevel.Warning);
    loggingBuilder.AddFilter("Azure", LogLevel.Warning);
});


// For development purposes. Reduces function polling exponential backoff
builder.UseEnvironment(EnvironmentName.Development);

var host = builder.Build();

using (host)
{
    await host.RunAsync();
}