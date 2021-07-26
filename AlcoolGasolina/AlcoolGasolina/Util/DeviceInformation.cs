using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AlcoolGasolina.Util
{
    public static class DeviceInformation
    {
        public static string CurentLanguage
        {
            get
            {
                try
                {
                    return CultureInfo.CurrentCulture.ToString();
                }
                catch
                {
                    return "pt-BR";
                }
            }
        }

        public static string CurrencySymbol
        {
            get
            {
                try
                {
                    return CultureInfo.GetCultureInfo(CurentLanguage).NumberFormat.CurrencySymbol;
                }
                catch
                {
                    return "R$";
                }
            }
        }
    }
}
