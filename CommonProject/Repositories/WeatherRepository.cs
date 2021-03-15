using CommonProject.Entities;
using CommonProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CommonProject.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        readonly WeatherDBContext _dbContext;

        public WeatherRepository(string connection)
        {
            _dbContext = new WeatherDBContext(connection);
        }

        public async Task AddOrUpdateRangeAsync(City city, IEnumerable<IDataConverter<WeatherInfo>> infoConverters)
        {
            City dbcity = await _dbContext.Cities.FirstOrDefaultAsync(c => string.Equals(c.Name, city.Name, StringComparison.OrdinalIgnoreCase));
            
            if (dbcity == null)
            {
               var entity =  _dbContext.Cities.Add(city);
               dbcity = entity.Entity;
            }

            foreach (var infoConverter in infoConverters)
            {
                var info = infoConverter.Convert();
                if (info is null)
                    continue;

                info.City = dbcity;

                WeatherInfo infoDb = await _dbContext.WeatherInfos.FirstOrDefaultAsync(s => DateTime.Equals(s.Date, info.Date)
                && info.City.Id == s.City.Id);

                if (infoDb == null)
                {
                    _dbContext.WeatherInfos.Add(info);
                }
                else
                {
                    _dbContext.WeatherInfos.Update(infoDb.Update(info));
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _dbContext.Cities.ToArrayAsync();
        }
        
        public async Task<IEnumerable<WeatherInfo>> GetWeatherInfoAsync(int cityId)
        {
            var result =  _dbContext.WeatherInfos.Where(s => s.City.Id == cityId).OrderBy(s => s.Date).ToArray();
            return result;
        }

        public async Task AddOrUpdateAsync(WeatherInfo weatherInfo)
        {
            if (weatherInfo is null || weatherInfo.City is null)
                return;

            City city = await _dbContext.Cities.FirstOrDefaultAsync(c => string.Equals(c.Name, weatherInfo.City.Name, StringComparison.OrdinalIgnoreCase));
            
            if (city is null)
            {
                _dbContext.WeatherInfos.Add(weatherInfo);
                await _dbContext.SaveChangesAsync();
                return;
            }

            weatherInfo.City = city;

            WeatherInfo info = await _dbContext.WeatherInfos.FirstOrDefaultAsync(s => DateTime.Equals(s.Date, weatherInfo.Date) && weatherInfo.City.Id == s.City.Id);
            
            if (info is null)
            {
                _dbContext.WeatherInfos.Add(weatherInfo);
                await _dbContext.SaveChangesAsync();
                return;
            }
            
            info.Update(weatherInfo);
            _dbContext.WeatherInfos.Update(info);
            await _dbContext.SaveChangesAsync();
        }
    }
}
