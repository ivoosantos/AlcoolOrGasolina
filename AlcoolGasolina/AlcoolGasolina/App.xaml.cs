using AlcoolGasolina.Businness;
using AlcoolGasolina.Businness.Interfaces;
using AlcoolGasolina.Database.Data;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlcoolGasolina
{
    public partial class App : Application
    {
        static PostoDataBase postoDatabase;

        public static PostoDataBase PostoDatabase
        {
            get
            {
                if (postoDatabase == null)
                {
                    postoDatabase = new PostoDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PostoDataBase.db3"));
                }

                return postoDatabase;
            }
        }

        public App()
        {
            InitializeComponent();
            Injection();
            MainPage = new View.Menu();
        }

        private void Injection()
        {
            DependencyService.Register<ILocaisCollectionBussinness, LocaisCollectionBusinness>();
            DependencyService.Register<IProcurarPostoViagemBusinness, ProcurarPostoViagemBusinness>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
