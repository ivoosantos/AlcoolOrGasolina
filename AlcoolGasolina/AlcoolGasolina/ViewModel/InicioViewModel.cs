using System;
using System.Collections.Generic;
using System.Text;
using AlcoolGasolina.Model;
using AlcoolGasolina.ViewModel;
using Xamarin.Forms;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AlcoolGasolina.ViewModel
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        Page _page;

        public event PropertyChangedEventHandler PropertyChanged;

        public Armazenamento Armazenamento { get; set; }
        public Command BuscaPostoCommand { get; set; }

        public InicioViewModel(Page page)
        {
            _page = page;
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            BuscaPostoCommand = new Command(async () => await BuscarPosto());
        }

        public void IniciarConsulta()
        {
            //App.Current.MainPage = new NavigationPage(new View.InserirDados());
            ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(new View.InserirDados());
        }

        public async Task BuscarPosto()
        {
            await _page.Navigation.PushAsync(new View.BuscarPostoView());
        }
    }
}
