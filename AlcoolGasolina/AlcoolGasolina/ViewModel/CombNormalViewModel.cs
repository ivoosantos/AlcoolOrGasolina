using AlcoolGasolina.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlcoolGasolina.ViewModel
{
    public class CombNormalViewModel
    {
        public Command NewConsult { get; set; }
        public Command BackInicio { get; set; }
        public string MessageCombustivel { get; set; }
        public string MessageCompensa { get; set; }
        public Decimal ValorCombustivel { get; set; }
        public Decimal InfoKmPorLitro { get; set; }
        public Decimal InfoKmTotalViagem { get; set; }

        public CombNormalViewModel()
        {
            Resultado();
            BackInicio = new Command(VoltarInicio);
            NewConsult = new Command(NovaConsulta);
        }

        public void Resultado()
        {
            ValorCombustivel = Decimal.Parse(Armazenamento.ValorCombustivel.ToString("F", CultureInfo.GetCultureInfo("pt-BR")));

            MessageCombustivel = "R$ " + ValorCombustivel;
            MessageCompensa = "Este é o valor que você \n vai gastar para sua viagem!";
        }

        public void NovaConsulta()
        {

            //App.Current.MainPage.Navigation.PopModalAsync(true);
            //(App.Current.MainPage).Navigation.PushModalAsync(new View.InserirDados(),true);
            //App.Current.MainPage = new View.InserirDados();
            App.Current.MainPage = new NavigationPage(new View.UmCombustivel());
        }
        public void VoltarInicio()
        {
            App.Current.MainPage.Navigation.PopModalAsync(true);
            //App.Current.MainPage.Navigation.PushModalAsync(new View.Menu(), true);
            App.Current.MainPage = new View.Menu();
            //App.Current.Quit();
        }
    }
}
