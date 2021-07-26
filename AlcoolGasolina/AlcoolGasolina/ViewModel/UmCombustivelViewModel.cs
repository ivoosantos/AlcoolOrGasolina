using System;
using System.Collections.Generic;
using System.Globalization;
using AlcoolGasolina.Model;
using System.Text;
using Xamarin.Forms;

namespace AlcoolGasolina.ViewModel
{
    public class UmCombustivelViewModel
    {
        public Command ResultCommand { get; set; }
        public Decimal ValorCombustivel { get; set; }
        public Decimal Valor { get; set; }
        public Decimal KmPorLitro { get; set; }
        public Decimal TotalViagem { get; set; }

        public UmCombustivelViewModel()
        {
            ResultCommand = new Command(Visualizar);
        }

        public void Visualizar()
        {
            try
            {
                Valor = Decimal.Parse(Armazenamento.ValCombustivel.Replace("R$", "").Replace(" ", ""));
                if (Valor > 0)
                {
                    Armazenamento.ValorCombustivel = CalcularCombustivel(Valor, KmPorLitro, TotalViagem);
                    App.Current.MainPage = new NavigationPage(new View.ResultCombNormal());
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Atenção!", "O valor do Combustível não pode ser 0(zero)!", "OK");
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("ERRO:", ex.ToString(), "OK");
            }
        }

        private static decimal CalcularCombustivel(decimal val, Decimal km, Decimal kmT)
        {
            Decimal Retorno = 0.00m;
            var div = kmT / km;
            if (div <= 1)
                Retorno = val;
            else
                Retorno = val * div;

            var language = CultureInfo.CurrentCulture.ToString();
            return (Decimal.Parse(Retorno.ToString("F", CultureInfo.GetCultureInfo(language))));
        }
    }
}
