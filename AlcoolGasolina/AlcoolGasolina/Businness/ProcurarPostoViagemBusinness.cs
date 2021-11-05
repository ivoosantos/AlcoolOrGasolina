using AlcoolGasolina.Businness.Interfaces;
using AlcoolGasolina.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoolGasolina.Businness
{
    public class ProcurarPostoViagemBusinness : IProcurarPostoViagemBusinness
    {
        public async Task UpdateFavorite(PostoModel postoModel)
        {
            List<PostoModel> lista = await App.PostoDatabase.GetPostosAsync();
            PostoModel item = lista.FirstOrDefault(x => x.Endereco.Equals(postoModel.Endereco));

            if (item == null)
            {
                _ = await App.PostoDatabase.SavePostoAsync(postoModel);
            }
            else
            {
                postoModel.ID = item.ID;
                _ = await App.PostoDatabase.UpdatePostoAsync(postoModel);
            }
        }
    }
}
