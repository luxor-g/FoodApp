using FoodApp.Models;
using System.Collections.Generic;
using System.Linq;
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
			var wordsToMatch = searchBar.Text.ToLower().Split(' ');
			var query = from receta in App.Recetas
						let w = receta.nombre.ToLower().Split(' ')
						where w.Intersect(wordsToMatch).Count() == wordsToMatch.Count()
						select receta;

			foreach (var receta in query)
				filtered.Add(receta);

			collectionView.ItemsSource = filtered;
		}
	}
}
