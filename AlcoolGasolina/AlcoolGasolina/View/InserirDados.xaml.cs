using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlcoolGasolina.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AlcoolGasolina.ViewModel;

namespace AlcoolGasolina.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InserirDados : ContentPage
    {
        public static bool resetValues = false;
        InserirDadosViewModel inserirDadosViewModel;

        public InserirDados()
        {
            InitializeComponent();
            inserirDadosViewModel = new InserirDadosViewModel(this);
            BindingContext = inserirDadosViewModel;

            KmViagemA.Text = "";
            KmViagemG.Text = "";
            KmViagem.Text = "";
        }

        protected override void OnAppearing()
        {
            if (resetValues)
            {
                inserirDadosViewModel.OnAppearingViewModel(resetValues);

                KmViagemA.Text = "";
                KmViagemG.Text = "";
                KmViagem.Text = "";

                resetValues = false;
            }
            
            base.OnAppearing();
        }
        //private void Alcool_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    AlcoolGasolina.Model.Armazenamento.ValAlcool = Alcool.Text;
        //}

        //private void Gasolina_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    AlcoolGasolina.Model.Armazenamento.ValGasolina = Gasolina.Text;
        //}
    }
}