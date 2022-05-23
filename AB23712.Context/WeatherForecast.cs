namespace AB23712.Context;

public class WeatherForecast
{
    public long Id { get; set; }
    public string ContactId { get; set; }

    public Status Status { get; set; }
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }
    
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public enum Status
{
    One,
    Two,
    Three,
    Four
}