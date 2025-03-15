namespace EShop
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public string? country { get; set; } = "Poland";
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
