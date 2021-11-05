using AlcoolGasolina.ViewModel;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static App5.Model.Business;

namespace AlcoolGasolina.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostosPopupView : PopupPage
    {
        ProcurarPostoViagemViewModel procurarPostoViagemViewModel;

        public PostosPopupView(ObservableCollection<Result> locais, List<Result> filterHandlerList)
        {
            InitializeComponent();
            procurarPostoViagemViewModel = new ProcurarPostoViagemViewModel(this, locais, filterHandlerList);

            BindingContext = procurarPostoViagemViewModel;
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            //picker.Focus();
        }

        protected override void OnAppearing()
        {
            procurarPostoViagemViewModel.OnAppearingViewModel();
            base.OnAppearing();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView cView = sender as CollectionView;
            Result ResultSelected = (Result)cView.SelectedItem;
            if (cView.SelectedItem != null)
            {
                procurarPostoViagemViewModel.SelectedItem = ResultSelected;
                cView.SelectedItem = null;
            }
        }

        private void FavoriteItemHandler_Clicked(object sender, EventArgs e)
        {
            try
            {
                ImageButton imageButton = null;
                Result result;

                if (sender.GetType().FullName.Contains("PancakeView"))
                {
                    Xamarin.Forms.PancakeView.PancakeView pancakeView = sender as Xamarin.Forms.PancakeView.PancakeView;
                    Grid grid = (Grid)pancakeView.Content;

                    imageButton = grid.Children.FirstOrDefault(x => x.GetType().FullName.Contains("ImageButton")) as ImageButton;
                    result = (Result)pancakeView.BindingContext;
                }
                else
                {
                    imageButton = sender as ImageButton;
                    result = (Result)imageButton.BindingContext;
                }

                if (!result.IsFavorite)
                {
                    result.IsFavorite = true;
                    imageButton.Source = ImageSource.FromFile("favorite_green.png");
                }
                else
                {
                    result.IsFavorite = false;
                    imageButton.Source = ImageSource.FromFile("no_favorite.png");
                }

                procurarPostoViagemViewModel.UpdateFavoriteItem(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}