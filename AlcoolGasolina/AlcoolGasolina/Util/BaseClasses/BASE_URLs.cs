using System;
using System.Collections.Generic;
using System.Text;

namespace AlcoolGasolina.Util
{
    public static class BASE_URLs
    {
        public const string googlePlaceUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&types=gas_station&name=&key={3}";
        public const string myKey = "AIzaSyCAF_Sp4FTfSt0TZlPCyx5KhnAYs48-rVQ";
    }
}
