using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlcoolGasolina.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultCombNormal : ContentPage
    {
        public ResultCombNormal()
        {
            InitializeComponent();
            BindingContext = new ViewModel.CombNormalViewModel();
        }
    }
}