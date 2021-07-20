using AlcoolGasolina.ViewModel;
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
    public partial class LocaisCollectionView : ContentPage
    {
        LocaisCollectionViewModel locaisListViewModel;

        public LocaisCollectionView()
        {
            InitializeComponent();
            locaisListViewModel = new LocaisCollectionViewModel(this);
            BindingContext = locaisListViewModel;
        }

        protected override void OnAppearing()
        {
            locaisListViewModel.OnAppearingViewModel();
            base.OnAppearing();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView cView = sender as CollectionView;

            if (cView.SelectedItem != null)
            {
                cView.SelectedItem = null;
            }
        }
    }
}