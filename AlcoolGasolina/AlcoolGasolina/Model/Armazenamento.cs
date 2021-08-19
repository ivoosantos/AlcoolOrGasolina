using System;
using System.Collections.Generic;
using System.Text;

namespace AlcoolGasolina.Model
{
    public class Armazenamento
    {
        public static Decimal Alcool { get; set; }
        public static Decimal Gasolina { get; set; }
        public static Decimal TotalViagem { get; set; }
        public static Decimal KmPorCarro { get; set; }
        public static string ValAlcool { get; set; }
        public static string ValGasolina { get; set; }
        public static string Mensagem { get; set; }
        public static string MensagemErro { get; set; }
        public static Decimal ValorCombustivel { get; set; }
        //public static string ValCombustivel { get; set; }
    }
}
