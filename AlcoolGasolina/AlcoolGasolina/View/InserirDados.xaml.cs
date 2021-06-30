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
        public InserirDados()
        {
            InitializeComponent();
            BindingContext = new InserirDadosViewModel();
            KmViagemA.Text = "";
            KmViagemG.Text = "";
            KmViagem.Text = "";
        }

        private void Alcool_TextChanged(object sender, TextChangedEventArgs e)
        {
            AlcoolGasolina.Model.Armazenamento.ValAlcool = Alcool.Text;
        }

        private void Gasolina_TextChanged(object sender, TextChangedEventArgs e)
        {
            AlcoolGasolina.Model.Armazenamento.ValGasolina = Gasolina.Text;
        }
    }
}