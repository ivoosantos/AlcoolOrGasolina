using AlcoolGasolina.Interface.ViewModel;
using AlcoolGasolina.Model;
using App5.DependencyServices;
using App5.Model;
using App5.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static App5.Model.Business;

namespace AlcoolGasolina.ViewModel
{
    public class LocaisCollectionViewModel : ViewModelClass, INotifyPropertyChanged
    {
        Page page;
        APIConnection<Business.Root> ApiConn = new APIConnection<Business.Root>();
        List<Result> locaisLista = null;

        public LocaisCollectionViewModel(Page page)
        {
            InitializeCommands();

            this.page = page;
        }

        private string selectedItemPicker;
        public string SelectedItemPicker
        {
            get { return selectedItemPicker; }
            set
            {
                if (selectedItemPicker != value)
                {
                    selectedItemPicker = value;
                    NotifyPropertyChanged(nameof(SelectedItemPicker));

                    _ = PesquisarClick();

                    LabelKm = $"Distância atual: {value}";
                }
            }
        }

        private string searchBarTxtValue;
        public string SearchBarTxtValue
        {
            get { return searchBarTxtValue; }
            set
            {
                if (searchBarTxtValue != value)
                {
                    searchBarTxtValue = value;
                    NotifyPropertyChanged(nameof(SearchBarTxtValue));

                    ApplyFilter(value);
                }
            }
        }

        private string[] distanciaItems;
        public string[] DistanciaItems
        {
            get => new string[] { "5 Km", "10 Km", "15 Km" };
            set
            {
                if (distanciaItems != value)
                {
                    distanciaItems = value;
                    NotifyPropertyChanged(nameof(DistanciaItems));
                }
            }
        }

        private double sliderValue;
        public double SliderValue
        {
            get { return sliderValue; }
            set
            {
                if (sliderValue != value)
                {
                    sliderValue = value;
                    NotifyPropertyChanged(nameof(SliderValue));
                }
            }
        }

        private string labelKm;
        public string LabelKm
        {
            get { return labelKm; }
            set
            {
                if (labelKm != value)
                {
                    labelKm = value;
                    NotifyPropertyChanged(nameof(LabelKm));
                }
            }
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
                    NotifyPropertyChanged(nameof(IsLoading));
                }
            }
        }

        private ObservableCollection<Result> locais;
        public ObservableCollection<Result> Locais
        {
            get { return locais; }
            set
            {
                if (locais != value)
                {
                    locais = value;
                    NotifyPropertyChanged(nameof(Locais));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand PesquisarClickCommand { get; set; }

        private void InitializeCommands()
        {
            PesquisarClickCommand = new Command(async () => await PesquisarClick());
        }

        private void InitializePicker()
        {
            SelectedItemPicker = "5 Km";
        }

        private async Task PesquisarClick()
        {
            try
            {
                StartLoading();

                await BuscarPostos();

                StopLoading();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task BuscarPostos()
        {
            try
            {
                var root = await ApiConn.GetAsync(AjustarStringToDouble().ToString());
                List<Result> listaResultadoPesquisa = root.results;

                if (listaResultadoPesquisa.Any())
                {
                    Locais = new ObservableCollection<Result>();

                    listaResultadoPesquisa = listaResultadoPesquisa.OrderByDescending(x => x.opening_hours != null && x.opening_hours.open_now)
                        .ThenByDescending(x => x.opening_hours != null && !x.opening_hours.open_now).ToList();

                    if (listaResultadoPesquisa == null || !listaResultadoPesquisa.Any())
                    {
                        listaResultadoPesquisa = root.results;
                    }

                    foreach (var item in listaResultadoPesquisa)
                    {
                        var resultFormatter = ResultFormatter(item);
                        Locais.Add(resultFormatter);
                    }

                    locaisLista = Locais.ToList();

                    CallToastMessage($"{Locais.Count} postos encontrados.");
                }
                else

                {
                    await page.DisplayAlert("Alerta", "Não foi encontrado nenhum posto!", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                StopLoading();
            }
        }

        private Result ResultFormatter(Result item)
        {
            item.ImgSource = "arrow_down.png";
            item.IsBoxViewVisible = true;
            item.IsImgOpenNowVisible = item.opening_hours == null ? false : true;
            item.IsOpenText = item.opening_hours == null ? string.Empty : item.IsOpenText;
            item.HexStatusGasStation = item.opening_hours != null && item.opening_hours.open_now ? "#008000" : "#FF0000";

            if (item.opening_hours != null && !item.opening_hours.open_now)
            {
                item.IsOpenText = "Fechado";
                item.ImgStatusPosto = "red_clock_icon.png";
            }
               
            return item;
        }

        public async Task GoToMap(Result value)
        {
            var options = new MapLaunchOptions
            {
                Name = value.vicinity,
                NavigationMode = NavigationMode.Driving
            };

            await Map.OpenAsync(value.geometry.location.lat, value.geometry.location.lng, options);
        }

        public void StartLoading()
        {
            IsLoading = true;
        }

        public void StopLoading()
        {
            IsLoading = false;
        }

        private double AjustarStringToDouble()
        {
            try
            {
                double resultado = double.Parse(SelectedItemPicker.Replace("Km", "").Trim()) * 1000;
                return resultado;
            }
            catch { return default; }
        }

        public void CallToastMessage(string message)
        {
            DependencyService.Get<IToastMessage>().ShowMessage(message);
        }

        private void ApplyFilter(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    Locais = new ObservableCollection<Result>();

                    foreach (var value in locaisLista)
                        Locais.Add(value);

                    return;
                }

                Locais = new ObservableCollection<Result>();

                var filteredItems = locaisLista.Where(x => x.name.ToUpperInvariant().Contains(text.ToUpperInvariant())).ToList();

                foreach (var value in filteredItems)
                {
                    Locais.Add(value);
                }
                    
            }
            catch (Exception ex)
            {
                ExceptionHandler(page);
                Debug.WriteLine(ex.Message);
            }
        }

        bool isFirstTime = true;
        public void OnAppearingViewModel()
        {
            if (isFirstTime)
            {
                InitializePicker();
                isFirstTime = false;
            }
        }

        public override Task ExceptionHandler(Page page)
        {
            return base.ExceptionHandler(page);
        }
    }
}
