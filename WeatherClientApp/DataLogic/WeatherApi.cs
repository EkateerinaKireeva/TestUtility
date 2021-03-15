using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherClientApp.DataLogic
{
    public class WeatherApi
    {
        private string _url;

        private static readonly Lazy<WeatherApi> lazyInstance
            = new Lazy<WeatherApi>(() => new WeatherApi());

        private WeatherApi()
        {
            _url = "https://localhost:44308/weatherforecast";
        }

        public Task<JobResult<CityDto[]>> GetCities(CancellationToken cancellationToken)
        {
           return HttpHelper.ExecuteGetRequestAsync<CityDto[]>($"{_url}/GetPopularCities", cancellationToken);
        }

        public Task<JobResult<WeatherInfoDto[]>> GetDetailInfo(int id, CancellationToken cancellationToken)
        {
            return HttpHelper.ExecuteGetRequestAsync<WeatherInfoDto[]>($"{ _url}/getweatherforcity/{id}", cancellationToken);
        }

        public static WeatherApi GetInstance()
        {
            return lazyInstance.Value;
        }
    }
}
