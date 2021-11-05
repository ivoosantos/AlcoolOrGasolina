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
        Page page;

        public Armazenamento Armazenamento { get; set; }

        private decimal valAlcool = 0m;
        private decimal valGasolina = 0m;

        private string valorAlcool;
        public string ValorAlcool
        {
            get { return valorAlcool; }
            set
            {
                if (valorAlcool != value)
                {
                    valorAlcool = value;
                    OnPropertyChanged(nameof(ValorAlcool));

                    if (!string.IsNullOrEmpty(value))
                    {
                        valAlcool = ConverterToDecimal(value);
                    }
                }
            }
        }

        private string valorGasolina;
        public string ValorGasolina
        {
            get { return valorGasolina; }
            set
            {
                if (valorGasolina != value)
                {
                    valorGasolina = value;
                    OnPropertyChanged(nameof(ValorGasolina));

                    if (!string.IsNullOrEmpty(value))
                    {
                        valGasolina = ConverterToDecimal(value);
                    }
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

        public Command ResultadoCommand { get; set; }

        private void InitializeCommands()
        {
            ResultadoCommand = new Command(VisualizarResultado);
        }

        public InserirDadosViewModel(Page page)
        {
            this.page = page;
            Armazenamento = new Armazenamento();

            InitializeCommands();
            SetInitialValues();
        }

        public void VisualizarResultado()
        {
            try
            {
                if (valAlcool <= 0 && valGasolina <= 0)
                {
                    page.DisplayAlert("Atenção!", "O valor do álcool ou gasolina não pode ser 0(zero)!", "OK");
                    return;
                }
                else if (KmAlcool <= 0 || KmGasolina <= 0)
                {
                    page.DisplayAlert("Atenção!", "O valor do Km álcool ou Km gasolina não pode ser 0(zero)!", "OK");
                    return;
                }

                Armazenamento.Alcool = CalcularAlcool(valAlcool, KmAlcool, TotalDaViagem);
                Armazenamento.Gasolina = CalcularGasolina(valGasolina, KmGasolina, TotalDaViagem);

                page.Navigation.PushAsync(new View.Resultado());
            }
            catch (Exception ex)
            {
                page.DisplayAlert("ERRO:", ex.ToString(), "OK");
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

            return decimal.Parse(Retorno.ToString("F", CultureInfo.GetCultureInfo(DeviceInformation.CurentLanguage)));
        }

        private static decimal CalcularGasolina(decimal vG, Decimal kmG, Decimal kmT)
        {
            Decimal Retorno = 0.00m;

            var div = kmT / kmG;

            if (div <= 1)
                Retorno = vG;
            else
                Retorno = vG * div;

            return decimal.Parse(Retorno.ToString("F", CultureInfo.GetCultureInfo(DeviceInformation.CurentLanguage)));
        }

        private decimal ConverterToDecimal(string str)
        {
            var language = CultureInfo.CurrentCulture.ToString();

            decimal result = 0m;
            if (decimal.TryParse(str.Replace(DeviceInformation.CurrencySymbol, "").Trim(), out result))
            {
                result = decimal.Parse(str.Replace(DeviceInformation.CurrencySymbol, ""), new CultureInfo(language));
            }
            else
            {
                result = 0m;
            }

            return result;
        }

        private void SetInitialValues()
        {
            ValorAlcool = "0";
            ValorGasolina = "0";
        }

        public void OnAppearingViewModel(bool resetValues)
        {
            if (resetValues)
            {
                ValorAlcool = "0";
                ValorGasolina = "0";
            }
        }
    }
}
