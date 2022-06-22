using FoodApp.Models;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
	internal class SearchViewModel : BaseViewModel
	{
		public Command ToRecipePage { get; set; }

		public SearchViewModel()
		{
			ToRecipePage = new Command<Receta>(OnNav);
		}

		private async void OnNav(Receta receta)
		{
			if (receta == null)
				return;

			await Shell.Current.GoToAsync(state: $"RecipePage?id={receta.id}");
		}
	}
}
