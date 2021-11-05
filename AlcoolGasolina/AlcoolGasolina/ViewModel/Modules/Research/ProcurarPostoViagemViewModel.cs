using AlcoolGasolina.Businness.Interfaces;
using AlcoolGasolina.Database.Model;
using AlcoolGasolina.Util.BaseClasses;
using App5.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static App5.Model.Business;

namespace AlcoolGasolina.ViewModel
{
    public class ProcurarPostoViagemViewModel : SearchPostoViewModelBase
    {
        Page page;

        private string imageFavorite;
        public string ImageFavorite
        {
            get {  return imageFavorite; }
            set
            {
                if (imageFavorite != value)
                {
                    imageFavorite = value;
                    OnPropertyChanged(nameof(ImageFavorite));
                }
            }
        }

        public ProcurarPostoViagemViewModel(Page page, ObservableCollection<Result> Locais)
        {
            Page = page;
            this.Locais = Locais;
        }

        public async void UpdateFavoriteItem(Result result)
        {
            try
            {
                PostoModel postoModel = new PostoModel
                {
                    Endereco = result.vicinity,
                    Latitude = "0",//result.geometry.location.lat.ToString(),
                    Longitude = "0",//result.geometry.location.lng.ToString(),
                    IsFavorite = result.IsFavorite,
                    Name = result.name
                };

                await DependencyService.Get<IProcurarPostoViagemBusinness>().UpdateFavorite(postoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
