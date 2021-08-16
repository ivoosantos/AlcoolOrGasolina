using AlcoolGasolina.Droid;
using AlcoolGasolina.View;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MapasView), typeof(MyTabbedPageRenderer))]
namespace AlcoolGasolina.Droid
{
    [Obsolete]
    public class MyTabbedPageRenderer : TabbedPageRenderer
    {
        public MyTabbedPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            if (e.NewElement == null || e.OldElement != null)
                return;

            Element.SelectedTabColor = System.Drawing.Color.FromArgb(0, 0, 0);
            Element.UnselectedTabColor = System.Drawing.Color.FromArgb(211, 211, 211);

            base.OnElementChanged(e);
        }
    }
}