using App5.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App5.Services
{
    public class APIConnection<T>
    {
        double latitude = 0;
        double longitude = 0;
        private const string myKey = "AIzaSyCAF_Sp4FTfSt0TZlPCyx5KhnAYs48-rVQ";

        public async Task<T> GetAsync(string distancia)
        {
            try
            {
                await GetDeviceLocation();
                string googlePlaceUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latitude},{longitude}&radius={distancia}&types=gas_station&name=&key={myKey}";

                googlePlaceUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-23.4746128,-46.7120028&radius=5000&types=gas_station&name=&key=AIzaSyCAF_Sp4FTfSt0TZlPCyx5KhnAYs48-rVQ";

                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(googlePlaceUrl);
                var results = JsonConvert.DeserializeObject<T>(json);

                return results;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(T);
            }
        }

        private async Task GetDeviceLocation()
        {
            try
            {
                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync();

                latitude = location.Latitude;
                longitude = location.Longitude;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
