using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}


		protected override void OnAppearing()
		{
			base.OnAppearing();
			collectionView.ItemsSource = GetFavoritos();
		}

		protected override async void OnDisappearing()
		{
			base.OnDisappearing();
			await Task.Delay(500);
			collectionView.ItemsSource = null;
		}

		List<Receta> GetFavoritos()
		{
			var favoritos = new List<Receta>();

			foreach (var receta in App.Recetas)
				if (String.Equals(receta.corazon, "icon_heart_filled.png"))
					favoritos.Add(receta);

			return favoritos;
		}

	}
}
