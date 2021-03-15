using CommonProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CommonProject.Repositories.Interfaces
{
    public interface IWeatherRepository
    {
        Task AddOrUpdateRangeAsync(City city, IEnumerable<IDataConverter<WeatherInfo>> weatherInfos);

        Task AddOrUpdateAsync(WeatherInfo weatherInfo);

        Task<IEnumerable<City>> GetCities();

        Task<IEnumerable<WeatherInfo>> GetWeatherInfoAsync(int cityId);
    }
}
