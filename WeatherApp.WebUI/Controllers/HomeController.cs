using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using WeatherApp.Application;
using WeatherApp.Repository.Interfaces;
using WeatherApp.WebUI.Models;

namespace WeatherApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWeatherRepository _repository;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IWeatherRepository repository)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> FetchData(RequestModel model)
        {

            try
            {
                var httpUrl =
                    $"https://api.weatherapi.com/v1/forecast.json?key=178aa88d186046c680d104052240902&q={model.city}&days={model.days}";

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get, httpUrl);

                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var contentStream =
                        await httpResponseMessage.Content.ReadAsStreamAsync();

                    var forecastResults = await JsonSerializer.DeserializeAsync
                        <ForecastViewModel>(contentStream);

                    if (forecastResults != null)
                    {
                        var weatherDto = new List<WeatherHistoryDTO>();

                        foreach (var item in forecastResults.forecast.forecastday)
                        {
                            weatherDto.Add(new WeatherHistoryDTO()
                            {
                                AvTempratureC = Convert.ToDecimal(item.day.avgtemp_c),
                                AvTempratureF = Convert.ToDecimal(item.day.avgtemp_f),
                                City = forecastResults.location.name,
                                Country = forecastResults.location.country,
                                ForecastDate = item.date.ToCustomDate(),
                                Humidity = item.day.avghumidity,
                                MaxWindKPH = Convert.ToDecimal(item.day.maxwind_kph),
                                MaxWindMPH = Convert.ToDecimal(item.day.maxwind_mph),
                                WeatherCondition = item.day.condition.text,
                            });
                        }

                        await _repository.AddForecasts(weatherDto);

                        return Json(new
                        {
                            error = false,
                            message = "Weather forecast saved"
                        });
                    }


                }
                else
                {
                    return Json(new
                    {
                        error = true,
                        message = $"Error"
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = true,
                    message = e.Message
                });
            }

            return Json(new
            {
                error = true,
                message = "An unknown error occured"
            });


        }

        public async Task<IActionResult> Chat()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> LoadChatData()
        {
            var forecastsList = await _repository.FetchWeatherHistories();

            var chatData = new List<ChatDataModel>();

            foreach (var item in forecastsList)
            {
                chatData.Add(new ChatDataModel()
                {
                    date = item.ForecastDate.ToString("dd-MMM-yyyy"),
                    value = item.AvTempratureF
                });
            }

            return Json(new { data = chatData });
        }

        //ChatDataModel

    }
}
