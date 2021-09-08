﻿using System;
using System.Collections.Generic;
using System.Globalization;
using AlcoolGasolina.Model;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlcoolGasolina.ViewModel
{
    public class UmCombustivelViewModel : INotifyPropertyChanged
    {
        Page _page;
        public Armazenamento Armazenamento { get; set; }
        public Command ResultCommand { get; set; }

        private Decimal valorCombustivel = 0m;
        private string valCombustivel;
        public string ValCombustivel
        {
            get { return valCombustivel; }
            set
            {
                if (valCombustivel != value)
                {
                    valCombustivel = value;
                    OnPropertyChanged(nameof(ValCombustivel));

                    if (!string.IsNullOrEmpty(value))
                    {
                        valorCombustivel = ConverterToDecimal(value);
                    }
                }
            }
        }

        public Decimal Valor { get; set; }

        private decimal kmPorlitro;
        public Decimal KmPorLitro { get; set; }
        public Decimal TotalViagem { get; set; }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void InitializeCommands()
        {
            ResultCommand = new Command(Visualizar);
        }

        public UmCombustivelViewModel(Page page)
        {
            _page = page;
            Armazenamento = new Armazenamento();

            InitializeCommands();
            SetInitialValues();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Visualizar()
        {
            try
            {
                if (valorCombustivel <= 0)
                {
                    _page.DisplayAlert("Atenção!", "O valor do Combustível não pode ser 0(zero)!", "OK");
                    return;
                }
                else if (KmPorLitro <= 0)
                {
                    _page.DisplayAlert("Atenção!", "O valor do Km por litro não pode ser 0(zero)!", "OK");
                    return;
                }

                Armazenamento.ValorCombustivel = CalcularCombustivel(valorCombustivel, KmPorLitro, TotalViagem);
                _page.Navigation.PushAsync(new View.ResultCombNormal());
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


        private decimal ConverterToDecimal(string str)
        {
            decimal result = 0m;
            if (decimal.TryParse(str.Replace("R$", "").Trim(), out result))
            {
                result = decimal.Parse(str.Replace("R$", ""), new CultureInfo("pt-BR"));
            }
            else
            {
                result = 0m;
            }

            return result;
        }


        private void SetInitialValues()
        {
            ValCombustivel = "0";
        }

        public void OnAppearingViewModel(bool resetValues)
        {
            if (resetValues)
            {
                ValCombustivel = "0";
            }
        }

    }
}
