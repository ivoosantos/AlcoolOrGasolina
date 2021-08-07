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
            menuLateral = new ObservableCollection<ItemMenuLateral>()
            {
                new ItemMenuLateral()
                {
                    Title = "Início",
                    Icon = "home01.png"
                },

                new ItemMenuLateral()
                {
                    Title = "Carro Flex",
                    Icon = "posto02.png"
                },

                new ItemMenuLateral()
                {
                    Title = "Álcool",
                    Icon = "iconAlcool03.png"
                },

                new ItemMenuLateral()
                {
                    Title = "Gasolina",
                    Icon = "iconGas03.png"
                },

                new ItemMenuLateral()
                {
                    Title = "Mapas",
                    Icon = "map_icon.png"
                }
            };

            //menuLateral.Add(page01);
            //menuLateral.Add(page02);
            //menuLateral.Add(page03);
            //menuLateral.Add(page04);
            //menuLateral.Add(page05);

            return menuLateral;
        }
    }
}

