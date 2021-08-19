using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AlcoolGasolina.Util
{
    public class MascaraMoeda : Behavior<Entry>
    {
        static string texto = null;
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }


        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool ehCallback = false;
                texto = args.NewTextValue;

                texto = HasNumber(texto);

                if (texto.Contains(DeviceInformation.CurrencySymbol))
                {
                    var valorNovoEmDecimal = ConverterReaisParaDecimal(texto);
                    var valorAntigoEmDecimal = args.OldTextValue.Contains(DeviceInformation.CurrencySymbol) ? ConverterReaisParaDecimal(args.OldTextValue) : int.Parse(args.OldTextValue);
                    ehCallback = valorNovoEmDecimal == valorAntigoEmDecimal;

                    texto = valorNovoEmDecimal.ToString();
                }

                if (!ehCallback)
                {
                    if (!string.IsNullOrEmpty(texto))
                    {
                        var textoFormatadoEmReais = (Decimal.Parse(texto) / 100).ToString("C", CultureInfo.GetCultureInfo(DeviceInformation.CurentLanguage));
                        texto = textoFormatadoEmReais;
                    }

                    ((Entry)sender).Text = texto;
                }
            }
        }

        private static int ConverterReaisParaDecimal(string valor)
        {
            valor = HasNumber(valor);

            var valorConvertido = Decimal.Parse(valor.Replace(DeviceInformation.CurrencySymbol, "").Replace(",", "").Replace(".", ""),
                CultureInfo.GetCultureInfo(DeviceInformation.CurentLanguage));

            return (int)valorConvertido;
        }

        public static string HasNumber(string text)
        {
            bool hasNumber = false;

            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    hasNumber = true;
                    break;
                }
            }

            return !hasNumber ? "0" : text;
        }
    }
}
