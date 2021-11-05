using AlcoolGasolina.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AlcoolGasolina.ViewModel
{
    public class CombNormalViewModel : INotifyPropertyChanged
    {
        private string messageCombustivel;
        public string MessageCombustivel
        {
            get { return messageCombustivel; }
            set
            {
                if (messageCombustivel != value)
                {
                    messageCombustivel = value;
                    OnPropertyChanged(nameof(MessageCombustivel));
                }
            }
        }

        public string MessageCompensa 
        {
            get { return "Este é o valor que você \nvai gastar para sua viagem!"; }
        }

        private Decimal valorCombustivel;
        public Decimal ValorCombustivel
        {
            get { return valorCombustivel; }
            set
            {
                if (valorCombustivel != value)
                {
                    valorCombustivel = value;
                    OnPropertyChanged(nameof(ValorCombustivel));
                }
            }
        }

        private Decimal infoKmPorLitro;
        public Decimal InfoKmPorLitro
        {
            get { return infoKmPorLitro; }
            set
            {
                if (infoKmPorLitro != value)
                {
                    infoKmPorLitro = value;
                    OnPropertyChanged(nameof(InfoKmPorLitro));
                }
            }
        }

        private Decimal infoKmTotalViagem;
        public Decimal InfoKmTotalViagem
        {
            get { return infoKmTotalViagem; }
            set
            {
                if (infoKmTotalViagem != value)
                {
                    infoKmTotalViagem = value;
                    OnPropertyChanged(nameof(InfoKmPorLitro));
                }
            }
        }

        public Command NewConsult { get; set; }
        
        public Command BackInicio { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CombNormalViewModel()
        {
            Resultado();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            BackInicio = new Command(VoltarInicio);
            NewConsult = new Command(NovaConsulta);
        }

        public void Resultado()
        {
            ValorCombustivel = Decimal.Parse(Armazenamento.ValorCombustivel.ToString("F", CultureInfo.GetCultureInfo("pt-BR")));

            MessageCombustivel = "R$ " + ValorCombustivel;
        }

        public void NovaConsulta()
        {
            App.Current.MainPage = new NavigationPage(new View.UmCombustivel());
        }
        public void VoltarInicio()
        {
            App.Current.MainPage.Navigation.PopModalAsync(true);
            App.Current.MainPage = new View.Menu();
        }
    }
}
