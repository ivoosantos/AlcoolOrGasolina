using AlcoolGasolina.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<ItemMenuLateral> MenuLateral;

        public Menu()
        {
            InitializeComponent();
            Detail = new NavigationPage(new Inicio());
            BindingContext = this;

            MenuLateral = new ObservableCollection<ItemMenuLateral>()
            {
                new ItemMenuLateral()
                {
                    Title = "Início",
                    Icon = "Home.png"
                },
                new ItemMenuLateral()
                {
                    Title = "Carro Flex",
                    Icon = "Gas.png"
                },
                new ItemMenuLateral()
                {
                    Title = "Carro Normal",
                    Icon = "Car.png"
                },
                new ItemMenuLateral()
                {
                    Title = "Mapas",
                    Icon = "Map.png"
                }
            };

            listview.ItemsSource = MenuLateral;
        }

        private void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ItemMenuLateral item = e.Item as ItemMenuLateral;

            switch (item.Title)
            {
                case "Início":
                    Detail = new NavigationPage(new Inicio());
                    IsPresented = false;
                    break;

                case "Carro Flex":
                    Detail = new NavigationPage(new InserirDados());
                    IsPresented = false;
                    break;

                case "Carro Normal":
                    Detail = new NavigationPage(new UmCombustivel());
                    IsPresented = false;
                    break;

                case "Mapas":
                    Detail = new NavigationPage(new MapasView());
                    IsPresented = false;
                    break;
            }
        }

        private void GoConfig(object sender, System.EventArgs e)
        {
            DisplayAlert("Atenção!", "Página em construção...", "OK");
            //Detail.Navigation.PushAsync(new View.Gasolina());
            IsPresented = false;
        }
    }
}