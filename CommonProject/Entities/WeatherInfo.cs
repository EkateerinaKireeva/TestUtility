using System;


namespace CommonProject.Entities
{
    public class WeatherInfo
    {
        public int Id { get; set; }

        public City City { get; set; }
        
        public DateTime Date { get; set; }

        public string MinTemperatureC { get; set; }

        public string MinTemperatureF { get; set; }

        public string MaxTemperatureC { get; set; }

        public string MaxTemperatureF { get; set; }


        public WeatherInfo Update(WeatherInfo weather)
        {  
            Date = weather.Date;
            MinTemperatureC = weather.MinTemperatureC;
            MinTemperatureF = weather.MinTemperatureF;
            MaxTemperatureC = weather.MaxTemperatureC;
            MaxTemperatureF = weather.MaxTemperatureF;
            return this;
        }
    }
}
