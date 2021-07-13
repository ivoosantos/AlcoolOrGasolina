using AlcoolGasolina.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AlcoolGasolina.Util;
using System.Globalization;
using AlcoolGasolina.Services;
using System.Collections.ObjectModel;
using AlcoolGasolina.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlcoolGasolina.ViewModel
{
    public class InserirDadosViewModel : INotifyPropertyChanged
    {
        public Armazenamento Armazenamento { get; set; }
        public Command ResultadoCommand { get; set; }

        private decimal valorAlcool;
        public decimal ValorAlcool 
        {
            get { return valorAlcool; }
            set
            {
                if (valorAlcool != value)
                {
                    valorAlcool = value;
                    OnPropertyChanged(nameof(ValorAlcool));
                }
            } 
        }

        private Decimal valorGasolina;
        public Decimal ValorGasolina 
        {
            get { return valorGasolina; }
            set
            {
                if (valorGasolina != value)
                {
                    valorGasolina = value;
                    OnPropertyChanged(nameof(ValorGasolina));
                }
            }
        }

        public Decimal TotalDaViagem { get; set; }
        public Decimal KmGasolina { get; set; }
        public Decimal KmAlcool { get; set; }
        public string ErroMensagem { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public InserirDadosViewModel()
        {
            Armazenamento = new Armazenamento();

            ResultadoCommand = new Command(VisualizarResultado);
        }

        public void VisualizarResultado()
        {
            try
            {
                //ValorAlcool = Decimal.Parse(Armazenamento.ValAlcool.Replace("R$", "").Replace(" ", ""));
                //ValorGasolina = Decimal.Parse(Armazenamento.ValGasolina.Replace("R$", "").Replace(" ", ""));

                if (ValorAlcool <= 0 && ValorGasolina <= 0)
                {
                    Armazenamento.MensagemErro = "O valor do álcool ou gasolina não pode ser 0(zero)!";
                    App.Current.MainPage.DisplayAlert("Atenção!", Armazenamento.MensagemErro, "OK");
                }
                else
                {
                    Armazenamento.Alcool = CalcularAlcool(ValorAlcool, KmAlcool, TotalDaViagem);
                    Armazenamento.Gasolina = CalcularGasolina(ValorGasolina, KmGasolina, TotalDaViagem);
                    //App.Current.MainPage.Navigation.PopModalAsync();
                    //App.Current.MainPage.Navigation.PushModalAsync(new Resultado(), true);
                    App.Current.MainPage = new NavigationPage(new View.Resultado());
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("ERRO:", ex.ToString(), "OK");
            }
        }

        private static decimal CalcularAlcool(decimal vA, Decimal kmA, Decimal kmT)
        {
            Decimal Retorno = 0.00m;
            var div = kmT / kmA;
            if (div <= 1)
                Retorno = vA;
            else
                Retorno = vA * div;
            
            return (Decimal.Parse(Retorno.ToString("F", CultureInfo.GetCultureInfo("pt-BR"))));
        }

        private static decimal CalcularGasolina(decimal vG, Decimal kmG, Decimal kmT)
        {
            Decimal Retorno = 0.00m;
            var div = kmT / kmG;
            if(div <= 1)
                Retorno = vG;
            else
                Retorno = vG * div;

            return (Decimal.Parse(Retorno.ToString("F", CultureInfo.GetCultureInfo("pt-BR"))));
        }
    }
}
