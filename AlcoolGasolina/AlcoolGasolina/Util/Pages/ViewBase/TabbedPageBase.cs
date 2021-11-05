using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace AlcoolGasolina.Util.Pages.ViewBase
{
    public class TabbedPageBase : Xamarin.Forms.TabbedPage
    {
        public Xamarin.Forms.TabbedPage tabbedPage { get; set; }

        public TabbedPageBase()
        {
            tabbedPage = new Xamarin.Forms.TabbedPage();
        }

        public virtual void AddContent()
        {
        }
    }
}
