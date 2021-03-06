using AlcoolGasolina.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;
using AlcoolGasolina.View;
using AlcoolGasolina.Util;

namespace AlcoolGasolina.ViewModel
{
    public class ResultadoViewModel : Armazenamento
    {
        Page Page;
        public Armazenamento Armazenamento { get; set; }
        public string Message { get; set; }
        public Decimal ValorAlcool { get; set; }
        public Decimal ValorGasolina { get; set; }
        public string MessageAlcool { get; set; }
        public string MessageGasolina { get; set; }
        public string MessageValAlcool { get; set; }
        public string MessageValGasolina { get; set; }

        public Command ResultCommand { get; set; }
        public Command VoltaInicio { get; set; }

        private void InitializeCommands()
        {
            ResultCommand = new Command(NovaConsulta);
            VoltaInicio = new Command(VoltarInicio);
        }

        public ResultadoViewModel(Page page)
        {
            this.Page = page;

            InitializeCommands();
            MostrarResultado();
        }

        public void MostrarResultado()
        {
            string currencySymbol = DeviceInformation.CurrencySymbol;

            ValorAlcool = Decimal.Parse(Alcool.ToString("F", CultureInfo.GetCultureInfo(DeviceInformation.CurentLanguage)));
            ValorGasolina = Decimal.Parse(Gasolina.ToString("F", CultureInfo.GetCultureInfo(DeviceInformation.CurentLanguage)));

            if(ValorAlcool > ValorGasolina)
            {
                MessageValAlcool = currencySymbol + ValorAlcool;
                MessageValGasolina = currencySymbol + ValorGasolina;
                MessageAlcool = "Compensa: Não";
                MessageGasolina = "Compensa: Sim";
            }
            else if(ValorAlcool < ValorGasolina)
            {
                MessageValAlcool = currencySymbol + ValorAlcool;
                MessageValGasolina = currencySymbol + ValorGasolina;
                MessageAlcool = "Compensa: Sim";
                MessageGasolina = "Compensa: Não";
            }
            else
            {
                MessageValAlcool = currencySymbol + ValorAlcool;
                MessageValGasolina = currencySymbol + ValorGasolina;
                MessageAlcool = "Compensa: Valor Igual";
                MessageGasolina = "Compensa: Valor Igual";
            }
        }

        public void NovaConsulta()
        {
            InserirDados.resetValues = true;
            this.Page.Navigation.PopAsync();

            //App.Current.MainPage = new NavigationPage(new View.InserirDados());
        }
        public void VoltarInicio()
        {
            App.Current.MainPage.Navigation.PopModalAsync(true);
            App.Current.MainPage = new View.Menu();
        }
    }
}
