using FoodApp.Models;
using FoodApp.Views;
using System;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{
		private string name;
		public string Name
		{
			get => name;
			set => SetProperty(ref name, value);
		}

		public Command RegisterCommand { get; }

		public RegisterViewModel()
		{
			RegisterCommand = new Command(OnSave);//, IsTextValid);
		}

		private async void OnSave(object obj)
		{
			User newUser = new User()
			{
				Id = Guid.NewGuid().ToString(),
				Name = Name,
				Email = "fdsfadfsa",
				Password = "fdsafdsafdsafds"
			};

			await UserDataStore.AddItemAsync(newUser);

			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}

		//private async void OnSave(string name, string email, string password)
	}
}
