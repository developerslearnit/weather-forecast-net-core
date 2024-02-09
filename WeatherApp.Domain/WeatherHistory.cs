using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Domain;

public class WeatherHistory
{
    public int WeatherHistoryId { get; set; }
    [StringLength(50)]
    public string Country { get; set; }
    [StringLength(50)]
    public string City { get; set; }
    public decimal AvTempratureC { get; set; }
    public decimal AvTempratureF { get; set; }
    public decimal MaxWindMPH { get; set; }
    public decimal MaxWindKPH { get; set; }
    public decimal Humidity { get; set; }
    [StringLength(500)]
    public string WeatherCondition { get; set; }
    public DateTime ForecastDate { get; set; }
}