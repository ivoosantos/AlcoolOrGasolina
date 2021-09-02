using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlcoolGasolina
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            MainPage = new View.Menu();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
