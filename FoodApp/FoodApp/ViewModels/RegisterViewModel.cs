using FoodApp.Models;
using FoodApp.Views;
using System;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{
		public Command RegisterCommand { get; }

		public RegisterViewModel()
		{
			RegisterCommand = new Command(OnSave, IsTextValid);
		}

		private async void OnSave(string name, string email, string password)
		{
			User newUser = new User()
			{
				Id = Guid.NewGuid().ToString(),
				Name = name,
				Email = email,
				Password = password
			};

			await DataStore.AddItemAsync(newUser);

			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
	}
}
