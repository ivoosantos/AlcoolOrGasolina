using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlcoolGasolina.Database.Model
{
    [Table("POSTOS")]
    public class PostoModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Endereco { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsFavorite { get; set; }
        public string ImgSource { get; set; } = "arrow_down.png";
        public string ImageSourceName { get; set; } = "no_favorite.png";
        public bool IsBoxViewVisible { get; set; } = true;
        public string Name { get; set; }
    }
}
