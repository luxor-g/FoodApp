using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
	internal class HomeViewModel : BaseViewModel
	{
		public Command ToRecipePage { get; set; }


		public HomeViewModel()
		{
			ToRecipePage = new Command<Receta>(OnNav);
		}

		private async void OnNav(Receta receta)
		{
			if(receta == null)
				return;

			await Shell.Current.GoToAsync(state: $"RecipePage?id={receta.id}");
		}
	}
}
