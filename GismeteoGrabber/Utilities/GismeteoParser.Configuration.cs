using CommonProject.Entities;
using CommonProject.Repositories.Interfaces;
using GismeteoGrabber.Utilities.Interfaces;
using GismeteoGrabber.Utilities.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace GismeteoGrabber.Utilities
{
    partial class GismeteoParser
    {
        public class GismeteoParserConfiguration : ParserConfiguration
        {
            public GismeteoParserConfiguration() : base("https://www.gismeteo.ru/")
            {
            }

            public string PopularCitiesListElementId { get => "noscript"; }

            public string InfoPartUrl { get => "10-days/"; }
        }

        public class CityData : IDataConverter<City>
        {
            public string Name { get; }
            public string Url { get; }

            public CityData(string name, string url)
            {
                Name = name;
                Url = url;
            }

            public City Convert()
            {
                return new City() { Name = Name };
            }
        }


        public class WeatherInfoData : IDataConverter<WeatherInfo>
        {
            public WeatherInfoData(CityData cityData, DateTime date)
            {
                City = cityData;
                Date = date;
            }


            public CityData City { get; }

            public DateTime Date { get; }

            public string MinTemperatureC { get; set; }

            public string MinTemperatureF { get; set; }

            public string MaxTemperatureC { get; set; }

            public string MaxTemperatureF { get; set; }


            public WeatherInfo Convert()
            {
                if (City is null)
                    return null;

                return new WeatherInfo()
                {
                    City = new City() { Name = City.Name },
                    Date = Date,
                    MaxTemperatureC = MaxTemperatureC,
                    MaxTemperatureF = MaxTemperatureF,
                    MinTemperatureF = MinTemperatureF,
                    MinTemperatureC = MinTemperatureC
                };
            }
        }
    }
}
