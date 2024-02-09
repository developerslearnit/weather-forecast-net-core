namespace WeatherApp.Application;

public class WeatherHistoryDTO
{
    public string Country { get; set; }
    public string City { get; set; }
    public decimal AvTempratureC { get; set; }
    public decimal AvTempratureF { get; set; }
    public decimal MaxWindMPH { get; set; }
    public decimal MaxWindKPH { get; set; }
    public decimal Humidity { get; set; }
    public string WeatherCondition { get; set; }
    public DateTime ForecastDate { get; set; }
}