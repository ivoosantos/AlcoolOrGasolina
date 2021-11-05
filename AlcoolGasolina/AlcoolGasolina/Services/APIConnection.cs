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
        public async Task<T> GetAsync(string parameter)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string googlePlaceUrl = parameter;
                    string json = await client.GetStringAsync(googlePlaceUrl);
                    T results = JsonConvert.DeserializeObject<T>(json);

                    return results;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
