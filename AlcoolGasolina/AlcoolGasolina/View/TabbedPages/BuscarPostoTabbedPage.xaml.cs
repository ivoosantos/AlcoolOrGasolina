using AlcoolGasolina.Util.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlcoolGasolina.View.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscarPostoTabbedPage : TabbedPage
    {
        public BuscarPostoTabbedPage()
        {
            InitializeComponent();
            Title = "Buscar Postos";
            AddContent();
        }

        private void AddContent()
        {
            ContentPage mapaView = new MapaView();
            mapaView.IconImageSource = ImageSource.FromFile("map_icon.png");
            mapaView.Title = "Mapa";
            tabbedPage.Children.Add(mapaView);

            ContentPage postoViagemFavoritoView = new LocaisCollectionView(Origin.BUSCAR_POSTOS);
            postoViagemFavoritoView.IconImageSource = ImageSource.FromFile("search_icon.png");
            postoViagemFavoritoView.Title = "Pesquisar";
            tabbedPage.Children.Add(postoViagemFavoritoView);
        }
    }
}