using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using App5.Services;
using App5.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App5.DependencyServices;

namespace AlcoolGasolina.View
{
    public partial class MapaView : ContentPage, INotifyPropertyChanged
    {
        string latitude = "0";
        string longitude = "0";
        APIConnection<Business.Root> getLocation = new APIConnection<Business.Root>();

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    NotifyPropertyChanged(nameof(IsLoading));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MapaView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async Task GetDeviceLocation()
        {
            try
            {
                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync();

                latitude = location.Latitude.ToString().Replace(",", ",");
                longitude = location.Longitude.ToString().Replace(",", ",");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        bool isFirstTime = true;

        protected override async void OnAppearing()
        {
            if (isFirstTime)
            {
                IsLoading = true;

                await CheckLocation();

                await GetDeviceLocation();

                await MapLocation();

                isFirstTime = false;

                IsLoading = false;
            }

            base.OnAppearing();
        }

        private async Task MapLocation()
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(
                                new Position(double.Parse(latitude), double.Parse(longitude)),
                                Distance.FromKilometers(1)));

            var result = await getLocation.GetAsync("1000");

            foreach (var item in result.results)
            {
                var pin = new Pin()
                {
                    Type = PinType.Place,
                    Position = new Position(item.geometry.location.lat, item.geometry.location.lng),
                    Label = item.name
                };

                Map.Pins.Add(pin);
            }
        }

        private async Task CheckLocation()
        {
            if (!DependencyService.Get<ILocation>().IsEnabled())
            {
                var resp = await DisplayAlert("Atenção", "Habilite seu GPS", "Configurações", "Cancelar");
                if (resp)
                {
                    await DependencyService.Get<ILocation>().OpenSettings();
                }
            }
        }
    }
}