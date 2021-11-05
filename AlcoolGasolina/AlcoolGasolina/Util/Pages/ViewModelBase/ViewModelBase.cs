using AlcoolGasolina.Util;
using App5.DependencyServices;
using App5.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlcoolGasolina.ViewModel
{
    public class ViewModelBase<T, T_A> : INotifyPropertyChanged
    {
        public Page Page {  get; set; }

        private readonly APIConnection<T> connection;
        public Command PesquisarCommand { get; set; }
        
        public ViewModelBase()
        {
            InitializeCommands();
            connection = new APIConnection<T>();
        }

        public virtual void InitializeCommands()
        {

        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void StartLoading()
        {
            IsLoading = true;
        }

        public virtual void StopLoading()
        {
            IsLoading = false;
        }

        public virtual void CallToastMessage(string message)
        {
            DependencyService.Get<IToastMessage>().ShowMessage(message);
        }

        public async Task<T> CallApiMethod(string parameter)
        {
            T model = await connection.GetAsync(parameter);
            return model;
        }

        public ObservableCollection<T_A> ToObservable(IEnumerable<T_A> lista)
        {
            ObservableCollection<T_A> toConvert = new ObservableCollection<T_A>();

            foreach (var item in lista)
                toConvert.Add(item);

            return toConvert;
        }
    }
}
