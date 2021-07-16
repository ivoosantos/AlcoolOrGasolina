using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlcoolGasolina.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : MasterDetailPage
    {
        public Menu()
        {
            InitializeComponent();
            Detail = new NavigationPage(new Inicio());
        }

        private void GoHome(object sender, System.EventArgs e)
        {
            //Detail.Navigation.PushAsync(new Inicio());
            Detail = new NavigationPage(new Inicio());
            IsPresented = false;
        }
        private void GoFlex(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new InserirDados());
            IsPresented = false;
            //Detail.Navigation.PushAsync(new InserirDados());

        }
        private void GoNormal(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new UmCombustivel());
            IsPresented = false;
            //Detail.Navigation.PushAsync(new UmCombustivel());
        }

        private void GoConfig(object sender, System.EventArgs e)
        {
            DisplayAlert("Atenção!", "Página em construção...", "OK");
            //Detail.Navigation.PushAsync(new View.Gasolina());
            IsPresented = false;
        }

        private void Sair(object sender, System.EventArgs e)
        {
            App.Current.Quit();
            IsPresented = false;
        }

        private void GoMapas(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new MapasView());
            IsPresented = false;
        }
    }
}