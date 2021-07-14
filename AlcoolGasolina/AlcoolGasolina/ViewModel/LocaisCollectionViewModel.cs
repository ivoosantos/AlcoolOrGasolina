using App5.DependencyServices;
using App5.Model;
using App5.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static App5.Model.Business;

namespace AlcoolGasolina.ViewModel
{
	public class LocaisCollectionViewModel : INotifyPropertyChanged
	{
		Page page;
		APIConnection<Business.Root> ApiConn = new APIConnection<Business.Root>();
		List<Result> locaisLista = null;
		
		public LocaisCollectionViewModel(Page page)
		{
			InitializeCommands();
			
			this.page = page;
		}

		private string selectedItemPicker;
		public string SelectedItemPicker
		{
			get { return selectedItemPicker; }
			set
			{
				if (selectedItemPicker != value)
				{
					selectedItemPicker = value;
					NotifyPropertyChanged(nameof(SelectedItemPicker));


					_ = PesquisarClick();
				}
			}
		}

		private string searchBarTxtValue;
		public string SearchBarTxtValue
		{
			get { return searchBarTxtValue; }
			set
			{
				if (searchBarTxtValue != value)
				{
					searchBarTxtValue = value;
					NotifyPropertyChanged(nameof(SearchBarTxtValue));

					ApplyFilter(value);
				}
			}
		}

		private string[] distanciaItems;
		public string[] DistanciaItems
		{
			get => new string[] { "1 KM", "2 KM", "3 KM", "4 KM", "5 KM", "6 KM", "7 KM", "8 KM", "9 KM", "10 KM" };
			set
			{
				if (distanciaItems != value)
				{
					distanciaItems = value;
					NotifyPropertyChanged(nameof(DistanciaItems));
				}
			}
		}

		private double sliderValue;
		public double SliderValue
		{
			get { return sliderValue; }
			set
			{
				if (sliderValue != value)
				{
					sliderValue = value;
					NotifyPropertyChanged(nameof(SliderValue));
				}
			}
		}

		private Result resultSelected;
		public Result ResultSelected
		{
			get { return resultSelected; }
			set
			{
				if (resultSelected != value)
				{
					resultSelected = value;
					_ = GetSelectedItem(value);
				}
			}
		}

		private bool isLoading;
		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				if (isLoading != value)
				{
					isLoading = value;
					NotifyPropertyChanged(nameof(IsLoading));
				}
			}
		}

		private ObservableCollection<Result> locais;
		public ObservableCollection<Result> Locais
		{
			get { return locais; }
			set
			{
				if (locais != value)
				{
					locais = value;
					NotifyPropertyChanged(nameof(Locais));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ICommand PesquisarClickCommand { get; set; }

		private void InitializeCommands()
		{
			PesquisarClickCommand = new Command(async () => await PesquisarClick());
		}

		private void InitializePicker()
		{
			SelectedItemPicker = "5 KM";
		}

		private async Task PesquisarClick()
		{
			try
			{
				StartLoading();

				await BuscarPostos();

				StopLoading();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public async Task BuscarPostos()
		{
			try
			{
				var root = await ApiConn.GetAsync(AjustarStringToDouble().ToString());
				List<Result> listaResultadoPesquisa = root.results;

				if (listaResultadoPesquisa.Any())
				{
					Locais = new ObservableCollection<Result>();

					foreach (var item in listaResultadoPesquisa)
						Locais.Add(item);

					locaisLista = Locais.ToList();

					CallToastMessage($"Total de: {Locais.Count} postos encontrados!");
				}
				else
				{
					await page.DisplayAlert("Alerta", "Não foi encontrado nenhum posto!", "Ok");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				StopLoading();
			}
		}

		public void ValueChangedViewModel(double value)
		{
			SliderValue = value;
		}

		private async Task GetSelectedItem(Result value)
		{
			var options = new MapLaunchOptions
			{
				Name = value.vicinity,
				NavigationMode = NavigationMode.Driving
			};

			await Map.OpenAsync(value.geometry.location.lat, value.geometry.location.lng, options);
		}

		//public override void DisableNavigationBar(Page page)
		//{
		//	base.DisableNavigationBar(page);
		//}
		//public override void GenericException(Page page)
		//{
		//	base.GenericException(page);
		//}
		public void StartLoading()
		{
			IsLoading = true;
		}

		public void StopLoading()
		{
			IsLoading = false;
		}

		private double AjustarStringToDouble()
		{
			double resultado = double.Parse(SelectedItemPicker.Replace("KM", "").Trim()) * 1000;
			return resultado;
		}

		public void CallToastMessage(string message)
		{
			DependencyService.Get<IToastMessage>().ShowMessage(message);
		}

		private void ApplyFilter(string text)
        {
            try
            {
				if (string.IsNullOrEmpty(text))
				{
					foreach (var value in locaisLista)
						Locais.Add(value);

					return;
				}

				Locais = new ObservableCollection<Result>();

				var filteredItems = locaisLista.Where(x => x.name.ToUpperInvariant().Contains(text.ToUpperInvariant())).ToList();

				foreach (var value in filteredItems)
					Locais.Add(value);
			}
			catch (Exception ex)
			{
                Debug.WriteLine(ex.Message);
			}
		}

		bool isFirstTime = true;
		public void OnAppearingViewModel()
        {
			if (isFirstTime)
            {
				InitializePicker();
				isFirstTime = false;
			}
        }
	}
}
