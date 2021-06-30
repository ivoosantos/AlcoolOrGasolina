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
        public UmCombustivel()
        {
            InitializeComponent();
            BindingContext = new UmCombustivelViewModel();
            KmViagemC.Text = "";
            KmViagemT.Text = "";
        }

        private void Combustivel_TextChanged(object sender, TextChangedEventArgs e)
        {
            AlcoolGasolina.Model.Armazenamento.ValCombustivel = Combustivel.Text;
        }
    }
}