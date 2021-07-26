using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlcoolGasolina.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlcoolGasolina.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UmCombustivel : ContentPage
    {
        public static bool resetValues = false;
        UmCombustivelViewModel umCombustivelViewModel;
        public UmCombustivel()
        {
            InitializeComponent();
            umCombustivelViewModel = new UmCombustivelViewModel(this);
            BindingContext = umCombustivelViewModel;

            KmViagemC.Text = "";
            KmViagemT.Text = "";
        }

        protected override void OnAppearing()
        {
            if (resetValues)
            {
                umCombustivelViewModel.OnAppearingViewModel(resetValues);

                KmViagemC.Text = "";
                KmViagemT.Text = "";

                resetValues = false;
            }
            base.OnAppearing();
        }

        //private void Combustivel_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //AlcoolGasolina.Model.Armazenamento.ValCombustivel = Combustivel.Text;
        //}
    }
}