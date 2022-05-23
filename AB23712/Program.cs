using System.Text.Json.Serialization;
using AB23712_SignalR.Context;
using ForecastUpdateHub = AB23712_SignalR.Hubs.ForecastUpdateHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddWeatherContext(builder.Configuration);
builder.Services.AddSignalR()
    .AddJsonProtocol(o => o.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", b => b
        .WithOrigins("https://localhost:44415")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// builder.Services.AddTransient<IUserIdProvider, ContactIdUserIdProvider>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapHub<ForecastUpdateHub>("/hub/connect");

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseCors("CorsPolicy");
app.Run();