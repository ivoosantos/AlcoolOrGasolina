using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AlcoolGasolina.Util.Converters
{
    public class StringToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nullable = value as decimal?;
            var result = string.Empty;

            if (nullable.HasValue)
            {
                result = nullable.Value.ToString();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            decimal intValue;
            decimal? result = null;

            if (stringValue.Contains("R$"))
                stringValue = stringValue.Replace("R$", "");

            try
            {
                result = System.Convert.ToDecimal(stringValue, new System.Globalization.CultureInfo("pt-BR"));
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return result;

            //if (decimal.TryParse(stringValue, out intValue))
            //{
            //    result = new Nullable<decimal>(intValue);
            //}
        }
    }
}
