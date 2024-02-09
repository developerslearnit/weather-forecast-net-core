using WeatherApp.Application;

namespace WeatherApp.Repository.Interfaces;

public interface IWeatherRepository
{
    Task<bool> AddForecasts(List<WeatherHistoryDTO> forecasts);

    Task<IReadOnlyList<WeatherHistoryDTO>> FetchWeatherHistories();
}