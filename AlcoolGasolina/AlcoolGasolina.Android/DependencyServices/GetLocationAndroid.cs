using AlcoolGasolina.Droid.DependencyServices;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App5.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AlcoolGasolina.Droid.DependencyServices.Location))]
namespace AlcoolGasolina.Droid.DependencyServices
{
    public class Location : ILocation
    {
        [Obsolete]
        LocationManager LM = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

        [Obsolete]
        public bool IsEnabled()
        {
            return LM.IsProviderEnabled(LocationManager.GpsProvider);
        }

        [Obsolete]
        public async void OpenSettings()
        {
            if (LM.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Context ctx = Forms.Context;
                ctx.StartActivity(new Intent(Android.Provider.Settings.ActionLocationSourceSettings));
            }
        }
    }
}