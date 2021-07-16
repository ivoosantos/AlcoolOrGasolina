using AlcoolGasolina.Droid.DependencyServices;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App5.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessageAndroid))]
namespace AlcoolGasolina.Droid.DependencyServices
{
	public class ToastMessageAndroid : IToastMessage
	{
		public void ShowMessage(string message)
		{
			Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
		}
	}
}