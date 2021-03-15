using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherClientApp.DataLogic;


namespace WeatherClientApp
{
    public partial class Form1 : Form
    {
        private CityDto[] _popularCities;
        private CancellationTokenSource _tokenSource;


        public Form1()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData()
        {
            RecreateToken();
            Task.Run(async() =>
            {
                await LoadDataAsync();

                if (_popularCities == null)
                    return;

                Action addCities = () =>
                {
                    popularCitiesListBox.Items.AddRange(_popularCities.Select(s => s.Name).ToArray());

                };
                popularCitiesListBox.BeginInvoke(addCities);
            });
        }


        private async Task LoadDataAsync()
        {
            var response = await WeatherApi.GetInstance().GetCities(_tokenSource.Token);
            
            if (!response.IsSuccessful || response.Content == null)
                return;

            _popularCities = response.Content;
        }


        private void RecreateToken()
        {
            if (_tokenSource != null)
                _tokenSource.Cancel();

            _tokenSource = new CancellationTokenSource();
        }
        

        private void popularCitiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = popularCitiesListBox.SelectedIndex;
           
            RecreateToken();
            WeatherCityForm new_Form = new WeatherCityForm(_popularCities[index], _tokenSource.Token);
            new_Form.Show();
        }
    }
}
