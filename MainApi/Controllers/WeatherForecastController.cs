using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonProject.Entities;
using CommonProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace MainApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherRepository weatherRepository)
        {
            _logger = logger;
            _weatherRepository = weatherRepository;
        }

   
        [HttpGet("GetPopularCities")]
        public async Task<ActionResult<City[]>> Get()
        {
            var popularCities = await _weatherRepository.GetCities();

            if (popularCities == null)
                return NotFound();

            return new ObjectResult(popularCities.ToArray());
        }


        [HttpGet("GetWeatherForCity/{id}")]
        public async Task<ActionResult<WeatherInfo>> GetWeatherForCity(int id)
        {
            var weatherInfo = await _weatherRepository.GetWeatherInfoAsync(id);

            if (weatherInfo == null)
                return NotFound();

            return new ObjectResult(weatherInfo);
        }
    }
}
