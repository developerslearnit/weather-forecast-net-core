using Microsoft.EntityFrameworkCore;
using WeatherApp.Application;
using WeatherApp.Domain;
using WeatherApp.Infrastructure;
using WeatherApp.Repository.Interfaces;

namespace WeatherApp.Repository;

public class WeatherRepository: IWeatherRepository
{
    private readonly WeatherDbContext _context;
    public WeatherRepository(WeatherDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddForecasts(List<WeatherHistoryDTO> forecasts)
    {

        var existingForecast = _context.WeatherHistories;

        if (existingForecast.Any())
        {
            _context.WeatherHistories.RemoveRange(existingForecast.ToList());
        }
        
        var weatherList = forecasts.Select(x=>new WeatherHistory()
        {
            AvTempratureC = x.AvTempratureC,
            AvTempratureF = x.AvTempratureF,
            City = x.City,
            Country = x.Country,
            ForecastDate = x.ForecastDate,
            Humidity = x.Humidity,
            MaxWindKPH = x.MaxWindKPH,
            MaxWindMPH = x.MaxWindMPH,
            WeatherCondition = x.WeatherCondition,

        });

        await _context.WeatherHistories.AddRangeAsync(weatherList);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IReadOnlyList<WeatherHistoryDTO>> FetchWeatherHistories()
    {
        return await _context.WeatherHistories.Select(x => new WeatherHistoryDTO()
        {
            AvTempratureC = x.AvTempratureC,
            AvTempratureF = x.AvTempratureF,
            City = x.City,
            Country = x.Country,
            ForecastDate = x.ForecastDate,
            Humidity = x.Humidity,
            MaxWindKPH = x.MaxWindKPH,
            MaxWindMPH = x.MaxWindMPH,
            WeatherCondition = x.WeatherCondition,
        }).ToListAsync();
    }
}