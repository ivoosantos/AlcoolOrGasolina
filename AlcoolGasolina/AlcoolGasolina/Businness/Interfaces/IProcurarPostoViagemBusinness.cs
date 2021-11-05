using AlcoolGasolina.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlcoolGasolina.Businness.Interfaces
{
    interface IProcurarPostoViagemBusinness
    {
        Task UpdateFavorite(PostoModel model);
    }
}
