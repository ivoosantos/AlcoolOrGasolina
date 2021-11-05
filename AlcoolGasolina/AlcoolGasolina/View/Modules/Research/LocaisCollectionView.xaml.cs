using AlcoolGasolina.Util.BaseClasses;
using AlcoolGasolina.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static App5.Model.Business;

namespace AlcoolGasolina.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocaisCollectionView : ContentPage
    {
        LocaisCollectionViewModel locaisListViewModel;

        public LocaisCollectionView(Origin origin)
        {
            InitializeComponent();
            locaisListViewModel = new LocaisCollectionViewModel(this, origin);
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

            Result ResultSelected = (Result)cView.SelectedItem;

            if (cView.SelectedItem != null)
            {
                locaisListViewModel.SelectedItem = ResultSelected;
                cView.SelectedItem = null;
            }
        }

        private async void GoToMap_Tapped(object sender, EventArgs e)
        {
            try
            {
                Frame frame = sender as Frame;

                await locaisListViewModel.GoToMap((Result)frame.BindingContext);
            }
            catch (Exception ex)
            {
            }
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            picker.Focus();
        }

        private void FavoriteItemHandler_Clicked(object sender, EventArgs e)
        {
            try
            {
                ImageButton imageButton = sender as ImageButton;
                Result result = (Result)imageButton.BindingContext;

                locaisListViewModel.FavoriteClickHandler(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}