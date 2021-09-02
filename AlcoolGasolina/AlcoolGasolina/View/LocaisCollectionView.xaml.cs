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

            Result ResultSelected = (Result)cView.SelectedItem;

            if (cView.SelectedItem != null)
            {
                Result item = FindItem(ResultSelected);

                int index = FindIndex(item);

                if (!((Result)cView.SelectedItem).IsSelected)
                {
                    item.IsSelected = true;
                    item.IsBoxViewVisible = false;
                    item.ImgSource = "arrow_up.png";
                    locaisListViewModel.Locais[index] = item;
                }
                else
                {
                    item.IsSelected = false;
                    item.IsBoxViewVisible = true;
                    item.ImgSource = "arrow_down.png";
                    locaisListViewModel.Locais[index] = item;
                }

                cView.SelectedItem = null;
            }
        }

        private async void GoToMap_Tapped(object sender, EventArgs e)
        {
            Frame frame = sender as Frame;

            await locaisListViewModel.GoToMap((Result)frame.BindingContext);
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            picker.Focus();
        }

        Result FindItem(Result result)
        {
            var itemResult = locaisListViewModel.Locais.Where(x => x.place_id == result.place_id).FirstOrDefault();
            return itemResult;
        }

        int FindIndex(Result result)
        {
            var item = locaisListViewModel.Locais.Where(x => x.place_id == result.place_id).FirstOrDefault();
            int index = locaisListViewModel.Locais.IndexOf(item);
            return index;
        }
    }
}