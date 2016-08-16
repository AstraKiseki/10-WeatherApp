using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherApp.Core;
using WeatherApp.Core.Services;
using System.Net;
using System.IO;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// The API key is 9f515ab125ef48a2 for this!
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var result = WeatherService.GetWeatherFor(SearchBox.Text);

            string fileUrl = $"{Environment.CurrentDirectory}/{result.icon}.gif";

            if (!File.Exists(fileUrl))
            {
                using (var webClient = new WebClient())
                {
                    byte[] bytes = webClient.DownloadData(result.icon_url);
                    File.WriteAllBytes(fileUrl, bytes);
                }
            }

            BitmapImage image = new BitmapImage(new Uri(fileUrl));

            WeatherImage.Source = image;

            // CityBlock.Text = result.full.ToString();
            LatiLongBlock.Text = result.latitude.ToString() + "/" + result.longitude.ToString();
            WeatherBlock.Text = result.weather.ToString();
            TempFBlock.Text = result.temp_f.ToString();
            TempCBlock.Text = result.temp_c.ToString();
            HumidityBlock.Text = result.relative_humidity.ToString();
            WindmphBlock.Text = result.wind_mph.ToString();
            WindDirectionBlock.Text = result.wind_dir.ToString();
            UVIndexBox.Text = result.UV.ToString();

        }
    }
}
