
using AlcoolGasolina.Businness;
using AlcoolGasolina.Businness.Interfaces;
using AlcoolGasolina.Database.Model;
using AlcoolGasolina.Model;
using AlcoolGasolina.Util;
using AlcoolGasolina.Util.BaseClasses;
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
    public class LocaisCollectionViewModel : SearchPostoViewModelBase
    {
        private readonly Origin Origin;
        private int LastCount { get; set;  }
        public ICommand PesquisarClickCommand { get; set; }

        private bool isImageFavoritoVisible;
        public bool IsImageFavoritoVisible
        {
            get {  return isImgKmVisible; }
            set
            {
                if (isImageFavoritoVisible != value)
                {
                    isImageFavoritoVisible = value;
                    OnPropertyChanged(nameof(IsImageFavoritoVisible));
                }
            }
        }

        private bool isImgKmVisible;
        public bool IsImgKmVisible
        {
            get { return isImgKmVisible; }
            set
            {
                if (isImgKmVisible != value)
                {
                    isImgKmVisible = value;
                    OnPropertyChanged(nameof(IsImgKmVisible));
                }
            }
        }

        public LocaisCollectionViewModel(Page page, Origin origin)
        {
            InitializeCommands();

            Page = page;
            Origin = origin;

            IsImageFavoritoVisible = Origin == Origin.FAVORITOS;
            IsImgKmVisible = Origin == Origin.BUSCAR_POSTOS;
        }

        private void InitializeCommands()
        {
            PesquisarClickCommand = new Command(async () => await PesquisarClick());
        }

        private async Task PesquisarClick()
        {
            try
            {
                if (Origin == Origin.BUSCAR_POSTOS)
                {
                    _ = await BuscarPostos();
                }
                else
                {
                    await GetFavoritos();
                }
            }
            catch (Exception ex)
            {
                await Page.DisplayAlert("Atenção", "Ocorreu um erro inesperado, tente novamente mais tarde!", "Ok");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task GetFavoritos()
        {
            List<PostoModel> postosFavoritos = await DependencyService.Get<ILocaisCollectionBussinness>().GetFavoritosLocais();

            foreach (var item in postosFavoritos)
            {
                PostoModel resultFavoritosFormatter = FavoritosFormatter(item);

                Result result = new Result
                {
                    IsFavorite = resultFavoritosFormatter.IsFavorite,
                    name = resultFavoritosFormatter.Name,
                    vicinity = resultFavoritosFormatter.Endereco,
                    ImgSource = resultFavoritosFormatter.ImgSource,
                    IsBoxViewVisible = resultFavoritosFormatter.IsBoxViewVisible,
                    IsImgOpenNowVisible = false,
                    IsOpenText = string.Empty
                };

                Locais.Add(result);
            }

            filterHandlerList = Locais.ToList();
            CallToastMessage($"{Locais.Count} postos salvos!");
        }

        private PostoModel FavoritosFormatter(PostoModel item)
        {
            item.ImgSource = "arrow_down.png";
            item.IsBoxViewVisible = true;
            return item;
        }

        public override async void PesquisarPostosAction()
        {
            StartLoading();

            try
            {
                var root = await BuscarPostos();

                List<Result> listaResultadoPesquisa = root.results;

                if (listaResultadoPesquisa.Any())
                {
                    Locais = new ObservableCollection<Result>();

                    listaResultadoPesquisa = listaResultadoPesquisa.OrderByDescending(x => x.opening_hours != null && x.opening_hours.open_now)
                        .ThenByDescending(x => x.opening_hours != null && !x.opening_hours.open_now).ToList();

                    if (listaResultadoPesquisa == null || !listaResultadoPesquisa.Any())
                        listaResultadoPesquisa = root.results;

                    foreach (var item in listaResultadoPesquisa)
                    {
                        var resultFormatter = ResultFormatter(item);
                        Locais.Add(resultFormatter);
                    }

                    filterHandlerList = Locais.ToList();

                    CallToastMessage($"{Locais.Count} postos encontrados.");
                }
                else
                {
                    await Page.DisplayAlert("Alerta", "Não foi encontrado nenhum posto!", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }

            StopLoading();
        }

        public override Result ResultFormatter(Result item)
        {
            item.IsImgOpenNowVisible = item.opening_hours == null ? false : true;
            item.IsOpenText = item.opening_hours == null ? string.Empty : item.IsOpenText;
            item.HexStatusGasStation = item.opening_hours != null && item.opening_hours.open_now ? "#008000" : "#FF0000";

            if (item.opening_hours != null && !item.opening_hours.open_now)
            {
                item.IsOpenText = "Fechado";
                item.ImgStatusPosto = "red_clock_icon.png";
            }

            item = base.ResultFormatter(item);
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

        bool isFirstTime = true;
        public override async void OnAppearingViewModel()
        {
            if (Origin == Origin.FAVORITOS)
            {
                bool changed = await DependencyService.Get<ILocaisCollectionBussinness>().FavotireItemsChanged();
                
                if (changed)
                {
                    PesquisarClickCommand.Execute(this);
                    isFirstTime = false;
                }
                else if (LastCount == 0)
                {
                    CallToastMessage("Nenhum posto favoritado!");
                }
                
                return;
            }

            if (isFirstTime)
            {
                SelectedItemPicker = "5 Km";
                isFirstTime = false;
            }
        }

        public async void FavoriteClickHandler(Result result)
        {
            List<PostoModel> items = await App.PostoDatabase.GetPostosAsync();
            PostoModel itemToRemove = items.FirstOrDefault(x => x.Endereco == result.vicinity);

            itemToRemove.IsFavorite = false;
            _ = await App.PostoDatabase.SavePostoAsync(itemToRemove);

            Locais.Remove(result);
        }
    }
}
