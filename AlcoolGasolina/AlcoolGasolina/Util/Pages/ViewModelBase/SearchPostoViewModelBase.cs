using AlcoolGasolina.ViewModel;
using App5.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using AlcoolGasolina.Database.Model;
using static App5.Model.Business;

namespace AlcoolGasolina.Util.BaseClasses
{
    public class SearchPostoViewModelBase : ViewModelBase<Business.Root, PostoModel>
    {
        private readonly List<Result> locaisLista = null;
        public List<PostoModel> ListaPostos = null;

        public string latitude { get; set; }
        public string longitude { get; set; }

        private double sliderValue;
        public double SliderValue
        {
            get { return sliderValue; }
            set
            {
                if (sliderValue != value)
                {
                    sliderValue = value;
                    OnPropertyChanged(nameof(SliderValue));
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
                    OnPropertyChanged(nameof(LabelKm));
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
                    OnPropertyChanged(nameof(SearchBarTxtValue));

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
                    OnPropertyChanged(nameof(DistanciaItems));
                }
            }
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
                    OnPropertyChanged(nameof(SelectedItemPicker));

                    PesquisarPostosAction();
                    LabelKm = $"Distância atual: {value}";
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
                    OnPropertyChanged(nameof(Locais));
                }
            }
        }

        private Result selectedItem;
        public Result SelectedItem
        {
            get { return selectedItem; }
            set
            {
                int index = Locais.IndexOf(value);

                if (!value.IsSelected)
                {
                    value.IsSelected = true;
                    value.IsBoxViewVisible = false;
                    value.ImgSource = "arrow_up.png";
                }
                else
                {
                    value.IsSelected = false;
                    value.IsBoxViewVisible = true;
                    value.ImgSource = "arrow_down.png";
                }

                Locais[index] = value;
                selectedItem = value;
            }
        }
        public Command PesquisarPostosCommand { get; set; }

        public SearchPostoViewModelBase()
        {
            InitializeCommands();
            Locais = new ObservableCollection<Result>();
        }

        public override void InitializeCommands()
        {
            PesquisarPostosCommand = new Command(() => PesquisarPostosAction());
        }

        public virtual void PesquisarPostosAction()
        {

        }

        public virtual async Task<Root> BuscarPostos()
        {
            try
            {
                StartLoading();

                await GetDeviceLocation();

                double kilometer = string.IsNullOrEmpty(SelectedItemPicker) ? 5 : double.Parse(SelectedItemPicker.Replace("Km", "").Trim());

                string parameterApi = string.Format(BASE_URLs.googlePlaceUrl, latitude, longitude, kilometer * 1000, BASE_URLs.myKey);

                var root = await CallApiMethod(parameterApi);

                StopLoading();

                return root;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task GetDeviceLocation()
        {
            try
            {
                Xamarin.Essentials.Location location = await Geolocation.GetLocationAsync();

                latitude = location.Latitude.ToString().Replace(",", ".");
                longitude = location.Longitude.ToString().Replace(",", ".");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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
                Debug.WriteLine(ex.Message);
            }
        }

        public virtual void OnAppearingViewModel()
        {
        }

        public virtual Result ResultFormatter(Result item)
        {
            item.ImgSource = "arrow_down.png";
            item.IsBoxViewVisible = true;
            return item;
        }

        public virtual List<PostoModel> GetPostos()
        {
            ListaPostos = App.PostoDatabase.GetPostosAsync().Result;
            return ListaPostos;
        }
    }
}
