using System;
using System.Collections.Generic;
using System.Text;
using AlcoolGasolina.Model;
using AlcoolGasolina.ViewModel;
using Xamarin.Forms;
using System.ComponentModel;

namespace AlcoolGasolina.ViewModel
{
    public class InicioViewModel
    {
        public Armazenamento Armazenamento { get; set; }
        public Command InicioCommand { get; set; }

        public InicioViewModel()
        {
            //InicioCommand = new Command(IniciarConsulta);
            
            //var Imagem = ImageSource.FromResource("AlcoolGasolina.img.logo_gas.png");
        }

        public void IniciarConsulta()
        {
            //App.Current.MainPage = new NavigationPage(new View.InserirDados());
            ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(new View.InserirDados());
        }

    }
}
