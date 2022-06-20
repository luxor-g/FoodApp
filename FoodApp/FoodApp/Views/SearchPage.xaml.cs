using FoodApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		public SearchPage()
		{
			InitializeComponent();
			searchBar.SearchCommand = new Command(SearchReceta);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			collectionView.ItemsSource = App.Recetas;
		}

		protected override async void OnDisappearing()
		{
			base.OnDisappearing();
			await Task.Delay(500);
			collectionView.ItemsSource = null;
			searchBar.Text = string.Empty;
		}

		private void SearchReceta()
		{
			if (string.IsNullOrEmpty(searchBar.Text))
			{
				collectionView.ItemsSource = App.Recetas;
				return;
			}

			var filtered = new List<Receta>();
			foreach (var receta in App.Recetas)
				if (receta.nombre.ToLower().Contains(searchBar.Text.ToLower()))
					filtered.Add(receta);

			collectionView.ItemsSource = filtered;
		}
	}
}
