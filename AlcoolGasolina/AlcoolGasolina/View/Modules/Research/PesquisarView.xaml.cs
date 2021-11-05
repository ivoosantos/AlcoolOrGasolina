using AlcoolGasolina.ViewModel;
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
    public partial class PesquisarView : ContentPage
    {
        PesquisarViewModel pesquisarViewModel = null;

        public PesquisarView()
        {
            InitializeComponent();
            pesquisarViewModel = new PesquisarViewModel(this);
            BindingContext = pesquisarViewModel;
        }
    }
}