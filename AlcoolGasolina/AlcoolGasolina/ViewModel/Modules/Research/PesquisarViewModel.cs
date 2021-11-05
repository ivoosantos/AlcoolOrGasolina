using AlcoolGasolina.Util.BaseClasses;
using AlcoolGasolina.View;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static App5.Model.Business;

namespace AlcoolGasolina.ViewModel
{
    public class PesquisarViewModel : SearchPostoViewModelBase
    {
        private bool isLocaisVisible;
        public bool IsLocaisVisible
        {
            get { return isLocaisVisible; }
            set
            {
                if (isLocaisVisible != value)
                {
                    isLocaisVisible = value;
                    OnPropertyChanged(nameof(IsLocaisVisible));
                }
            }
        }

        private string addressEntry;
        public string AddressEntry
        {
            get { return addressEntry; }
            set
            {
                if (addressEntry != value)
                {
                    addressEntry = value;
                    OnPropertyChanged(nameof(AddressEntry));
                }
            }
        }

        public PesquisarViewModel(Page page)
        {
            Page = page;
        }

        public override async void PesquisarPostosAction()
        {
            try
            {
                if (string.IsNullOrEmpty(AddressEntry))
                {
                    await Page.DisplayAlert("Atenção", "O endereço deve ser preenchido!", "Ok");
                    return;
                }

                var root = await BuscarPostos();

                List<Result> listaResultadoPesquisa = new List<Result>
                {
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 239"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 240"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 2390"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 2392"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 2"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 55"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 56"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 57"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 70"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 500"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 800"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 351"
                    },
                    new Result
                    {
                        name = "Posto Shell",
                        vicinity = "Rua Dr. Edmundo Bitencourt, 222"
                    }
                };

                if (listaResultadoPesquisa.Any())
                {
                    foreach (Result item in listaResultadoPesquisa)
                    {
                        Result resultFormatter = ResultFormatter(item);
                        Locais.Add(resultFormatter);
                    }

                    IsLocaisVisible = true;
                }
                else
                {
                    await Page.DisplayAlert("Alerta", "Não foi encontrado nenhum posto!", "Ok");
                    IsLocaisVisible = false;
                }

                CallToastMessage($"{Locais.Count} postos encontrados.");

                var popUpPage = new PostosPopupView(Locais);
                await PopupNavigation.Instance.PushAsync(popUpPage);
            }
            catch (Exception ex)
            {
                await Page.DisplayAlert("Atenção", "Algo deu errado, tente novamente mais tarde!", "Ok");
                Console.WriteLine(ex.Message);
            }
        }

        public override async Task GetDeviceLocation()
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(AddressEntry);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    latitude = location.Latitude.ToString();
                    longitude = location.Longitude.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override Result ResultFormatter(Result item)
        {
            item.IsFavorite = GetPostos().Exists(x => x.Endereco.Equals(item.vicinity) && x.IsFavorite);
            item.ImageSourceName = item.IsFavorite ? "favorite_green.png" : "no_favorite.png";
            return item;
        }
    }
}
