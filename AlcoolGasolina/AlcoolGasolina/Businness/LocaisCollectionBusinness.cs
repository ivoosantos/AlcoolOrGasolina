using AlcoolGasolina.Businness.Interfaces;
using AlcoolGasolina.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoolGasolina.Businness
{
    public class LocaisCollectionBusinness : ILocaisCollectionBussinness
    {
        int LastCount = 0;

        public async Task<bool> FavotireItemsChanged()
        {
            var favoriteItems = await App.PostoDatabase.GetPostosAsync();

            int countFavoriteItems = favoriteItems.Count(x => x.IsFavorite);
            bool changedFavoriteItems = LastCount != countFavoriteItems;

            LastCount = countFavoriteItems;

            return changedFavoriteItems;
        }

        public async Task<List<PostoModel>> GetFavoritosLocais()
        {
            List<PostoModel> items = await App.PostoDatabase.GetPostosAsync();
            var postosFavoritos = items.Where(x => x.IsFavorite).ToList();

            return postosFavoritos;
        }
    }
}
