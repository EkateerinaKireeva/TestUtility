using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherClientApp.DataLogic;

namespace WeatherClientApp
{
    public partial class WeatherCityForm : Form
    {
        private int _cityId;
        private CancellationToken _cancellationToken;

        public WeatherCityForm(CityDto city, CancellationToken cancellationToken)
        {
            InitializeComponent();
            cityNameLabel.Text = city.Name;
            _cityId = city.Id;
            _cancellationToken = cancellationToken;
            LoadData();
        }

        private void LoadData()
        {
            Task.Run(async () =>
            {
                WeatherInfoDto[] weatherInfos = await LoadDataAsync();
                                
                 Action addCities = () =>
                 {
                   weatherList.Items.AddRange(weatherInfos.Select(s => s.GetDescription()).ToArray());
                 };

                weatherList.BeginInvoke(addCities);
            });
        }

        private async Task<WeatherInfoDto[]> LoadDataAsync()
        {
            var response = await WeatherApi.GetInstance().GetDetailInfo(_cityId, _cancellationToken);

            if (!response.IsSuccessful || response.Content == null)
                return null;

            return response.Content;
        }    
    }
}
