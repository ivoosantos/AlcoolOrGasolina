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
    public partial class ViagemTabbedPage : TabbedPage
    {
        public ViagemTabbedPage()
        {
            InitializeComponent();
            Title = "Viagem";
            AddContent();
        }

        private void AddContent()
        {
            ContentPage pesquisarView = new PesquisarView();
            pesquisarView.IconImageSource = ImageSource.FromFile("travel_icon.png");
            pesquisarView.Title = "Procurar";
            tabbedPage.Children.Add(pesquisarView);

            ContentPage postoViagemFavoritoView = new LocaisCollectionView(Origin.FAVORITOS);
            postoViagemFavoritoView.IconImageSource = ImageSource.FromFile("favorite.png");
            postoViagemFavoritoView.Title = "Favoritos";
            tabbedPage.Children.Add(postoViagemFavoritoView);
        }
    }
}