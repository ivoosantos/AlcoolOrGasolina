using AlcoolGasolina;
using AlcoolGasolina.View;
using App5.DependencyServices;
using App5.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace App5.Services
{
    public class APIConnection<T>
    {
        double latitude = 0;
        double longitude = 0;
        private const string myKey = "AIzaSyCAF_Sp4FTfSt0TZlPCyx5KhnAYs48-rVQ";

        string testeLat = null;
        string testeLong = null;
        public async Task<T> GetAsync(string distancia)
        {
            try
            {
                await GetDeviceLocation();
                string googlePlaceUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={testeLat},{testeLong}&radius={distancia}&types=gas_station&name=&key={myKey}";

                HttpClient httpClient = new HttpClient();
                string json = await httpClient.GetStringAsync(googlePlaceUrl);
                T results = JsonConvert.DeserializeObject<T>(json);

                return results;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default;
            }
        }

        private async Task GetDeviceLocation()
        {
            try
            {
                if (!DependencyService.Get<ILocation>().IsEnabled())
                {
                    DependencyService.Get<ILocation>().OpenSettings().Wait();
                }

                Location location = await Geolocation.GetLocationAsync();

                latitude = location.Latitude;
                longitude = location.Longitude;
                testeLat = latitude.ToString().Replace(",", ".");
                testeLong = longitude.ToString().Replace(",", ".");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
