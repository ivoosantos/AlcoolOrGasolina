using AlcoolGasolina.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AlcoolGasolina.Services
{
    public class ItemsMenu
    {
        private static ObservableCollection<ItemMenuLateral> menuLateral { get; set; }

        public static ObservableCollection<ItemMenuLateral> GetMenuItens()
        {
            menuLateral = new ObservableCollection<ItemMenuLateral>();

            var page01 = new ItemMenuLateral() { Title = "Início", Icon = "home01.png", TargetType = typeof(View.Inicio) };
            var page02 = new ItemMenuLateral() { Title = "Carro Flex", Icon = "posto02.png", TargetType = typeof(View.InserirDados) };
            var page03 = new ItemMenuLateral() { Title = "Álcool", Icon = "iconAlcool03.png", TargetType = null };
            var page04 = new ItemMenuLateral() { Title = "Gasolina", Icon = "iconGas03.png", TargetType = null };

            menuLateral.Add(page01);
            menuLateral.Add(page02);
            menuLateral.Add(page03);
            menuLateral.Add(page04);

            return menuLateral;

        }

    }
}
