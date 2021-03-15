using System;


namespace WeatherClientApp.DataLogic
{
    public class WeatherInfoDto
    {
        public DateTimeOffset Date { get; set; }

        public string MinTemperatureC { get; set; }

        public string MinTemperatureF { get; set; }

        public string MaxTemperatureC { get; set; }

        public string MaxTemperatureF { get; set; }
    }

    public static class WeatherInfoDtoExtension
    {
        public static string GetDescription(this WeatherInfoDto infoDto)
        {
            return $"День: {infoDto.Date.ToLocalTime().Day.ToString()} Мин. тепр. {infoDto.MinTemperatureC} Макс. темп. {infoDto.MaxTemperatureC}";
        }
    }
}
