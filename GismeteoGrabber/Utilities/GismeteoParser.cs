using CommonProject.Repositories.Interfaces;
using GismeteoGrabber.Utilities.Interfaces;
using GismeteoGrabber.Utilities.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GismeteoGrabber.Utilities
{
    public partial class GismeteoParser : IParser
    {
        private readonly IRequestHandler _requestHandler;
        private readonly IWeatherRepository _weatherRepository;
        private readonly GismeteoParserConfiguration _config;

        public GismeteoParser(IRequestHandler requestHandler, IWeatherRepository weatherRepository)
        {
            _requestHandler = requestHandler;
            _config = new GismeteoParserConfiguration();
            _weatherRepository = weatherRepository;
        }

        ParserConfiguration IParser.Configuration { get => _config; }

        async Task IParser.ParseDataAsync()
        {
            JobResult<CityData[]> citiesJobResult = GetPopularCities();
            
            if (citiesJobResult.IsSuccessful is false || citiesJobResult.Content.IsNullOrEmpty())
                return;

            var datas = from n in citiesJobResult.Content.AsParallel()
                              select (jobResult: GetWeatherInfo(n), city: n);


            foreach (var item in datas.ToList())
            {
                var jobResult = item.jobResult;

                if (jobResult.IsSuccessful is false || jobResult.Content.IsNullOrEmpty())
                    return;

                await _weatherRepository.AddOrUpdateRangeAsync(item.city.Convert(), jobResult.Content);
            }
        }


        private JobResult<CityData[]> GetPopularCities()
        {
            try
            {
                var response = _requestHandler.Load(_config.Url);

                if (response.IsSuccessful is false || response.Content is null)
                    return JobResult<CityData[]>.CreateFailed();

                var citiesNodes = response.Content.GetElementbyId(_config.PopularCitiesListElementId)?
                    .SelectNodes(".//a");

                if (citiesNodes == null)
                    return JobResult<CityData[]>.CreateFailed();

                var cities = citiesNodes.Select(n => new CityData(n.Attributes["data-name"]?.Value, n.Attributes["href"]?.Value)).
                    Where(c => !string.IsNullOrEmpty(c.Url) && !string.IsNullOrEmpty(c.Name)).ToArray();

                if (cities.Length == 0)
                    return JobResult<CityData[]>.CreateFailed();

                return JobResult<CityData[]>.CreateSuccessful(cities);
            }
            catch
            {
                return JobResult<CityData[]>.CreateFailed();
            }
        }


        private JobResult<IEnumerable<WeatherInfoData>> GetWeatherInfo(CityData cityData)
        {
            if(cityData is null)
                return JobResult<IEnumerable<WeatherInfoData>>.CreateFailed();

            string ConfigurateUrl() => $"{_config.Url}{cityData.Url}{_config.InfoPartUrl}";

            List<WeatherInfoData> infoData = new List<WeatherInfoData>();
            try
            {
                var response = _requestHandler.Load(ConfigurateUrl());

                if (response.IsSuccessful is false || response.Content is null)
                    return JobResult<IEnumerable<WeatherInfoData>>.CreateFailed();


                var temperatureNodes = response.Content.DocumentNode.SearchNode("div", "class", "templine w_temperature").SearchNodes("div", "class", "value");

                if(temperatureNodes is null)
                    return JobResult<IEnumerable<WeatherInfoData>>.CreateFailed();

                DateTime currentDate = DateTime.UtcNow.Date;

                foreach (var item in temperatureNodes)
                {
                    var maxNode  =  item.SearchNode("div", "class", "maxt");
                    var minNode = item.SearchNode("div", "class", "mint");

                    //TODO parse from web
                    WeatherInfoData weatherInfo = new WeatherInfoData(cityData, currentDate);
                    currentDate = currentDate.AddDays(1);
                 
                    weatherInfo.MaxTemperatureC = maxNode.SearchNode("span", "class", "unit unit_temperature_c")?.InnerHtml.ClearTemperature();
                    weatherInfo.MaxTemperatureF = maxNode.SearchNode("span", "class", "unit unit_temperature_f")?.InnerHtml.ClearTemperature();

                    weatherInfo.MinTemperatureC = minNode.SearchNode("span", "class", "unit unit_temperature_c")?.InnerHtml.ClearTemperature();
                    weatherInfo.MinTemperatureF = minNode.SearchNode("span", "class", "unit unit_temperature_f")?.InnerHtml.ClearTemperature();

                    infoData.Add(weatherInfo);
                }  
                
                return JobResult<IEnumerable<WeatherInfoData>>.CreateSuccessful(infoData);             
            }
            catch
            {
                return JobResult<IEnumerable<WeatherInfoData>>.CreateFailed();
            }
        }
    }
}
