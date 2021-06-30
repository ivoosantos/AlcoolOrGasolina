using AlcoolGasolina.Model;
using AlcoolGasolina.Services;
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
    public partial class MenuLateral : MasterDetailPage
    {
        private ObservableCollection<ItemMenuLateral> _menuLista;
        public MenuLateral()
        {
            InitializeComponent();
            _menuLista = ItemsMenu.GetMenuItens();
            listview.ItemsSource = _menuLista;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Inicio)));
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (ItemMenuLateral)e.SelectedItem;
            Type pagina = item.TargetType;
            if (pagina != null)
                Detail = new NavigationPage((Page)Activator.CreateInstance(pagina));
            else
                DisplayAlert("Atenção!", "Página ainda em construção...", "Ok");

            IsPresented = false;
        }
    }
}