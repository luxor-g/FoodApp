using FoodApp.Models;
using FoodApp.Services;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartUpPage : ContentPage
	{
		public StartUpPage()
		{
			InitializeComponent();
		}

		async void GoToLogin(object sender, EventArgs args) => await Navigation.PushAsync(new LoginPage());

		async void GoToRegister(object sender, EventArgs args) => await Navigation.PushAsync(new RegisterPage());


		protected override async void OnAppearing()
		{
			base.OnAppearing();

			string userPath = Path.Combine(App.Path, "user");
			string voicePath = Path.Combine(App.Path, "voice");

			if (File.Exists(userPath))
			{
				App.loggedUser = int.Parse(File.ReadAllText(userPath));

				if (await App.restService.GetRecetasAsUsuario(new Usuario { id = App.loggedUser }))
					await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
				else
					await Application.Current.MainPage.DisplayAlert("Alerta", "Ocurrió un problema inesperado al intentar " +
						"cargar las recetas, por favor vuelva a intentarlo más tarde.", "Aceptar");
			}

			if (File.Exists(voicePath))
			{
				App.speechService.Voice = File.ReadAllText(voicePath);
			}
			else
			{
				App.speechService.Voice = SpeechService.FemaleVoice;
				File.WriteAllText(voicePath, App.speechService.Voice);
			}
		}
	}
}
