using AlcoolGasolina.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlcoolGasolina.Businness.Interfaces
{
    public interface ILocaisCollectionBussinness
    {
        Task<List<PostoModel>> GetFavoritosLocais();
        Task<bool> FavotireItemsChanged();
    }
}
