using FoodApp.Models;
using FoodApp.Services;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

		private async void ChangeName(object sender, EventArgs e)
		{
			string newNombre = await DisplayPromptAsync(title: "Cambiar Nombre", message: "Introduzca su nuevo nombre.",
				accept: "Aceptar", cancel: "Cancelar", maxLength: 256, keyboard: Keyboard.Email);

			if (!await App.restService.UpdateUsuario(new Usuario() { id = App.loggedUser, nombre = newNombre }))
				await DisplayAlert("Alerta", "Se produjo un error al intentar cambiar el email.", "Aceptar");
		}

		private async void ChangeEmail(object sender, EventArgs e)
		{
			string newEmail = await DisplayPromptAsync(title: "Cambiar Email", message: "Introduzca su nuevo email.",
				accept: "Aceptar", cancel: "Cancelar", maxLength: 254, keyboard: Keyboard.Email);

			if (!await App.restService.UpdateUsuario(new Usuario() { id = App.loggedUser, email = newEmail }))
				await DisplayAlert("Alerta", "Se produjo un error al intentar cambiar el email.", "Aceptar");
		}

		private async void ChangePassword(object sender, EventArgs e)
		{
			string newClave = await DisplayPromptAsync(title:"Cambiar Contraseña", message:"Introduzca su nueva contraseña.",
				accept:"Aceptar", cancel:"Cancelar", maxLength:32, keyboard:Keyboard.Text);

			if(!await App.restService.UpdateUsuario(new Usuario() { id=App.loggedUser, clave=newClave}))
				await DisplayAlert("Alerta", "Se produjo un error al intentar cambiar la contraseña.", "Aceptar");
		}

		private async void LogoutClicked(object sender, EventArgs e)
		{
			if(await DisplayAlert("Cerrar Sesión", "¿Está seguro de que quiere cerrar sesión?", "Sí", "No"))
				OnLogout();
		}

		private async void DeleteAccount(object sender, EventArgs e)
		{
			if (await DisplayAlert("Borrar Cuenta", "¿Está seguro de que quiere borrar su cuenta?", "Sí", "No"))
			{
				if (!await DisplayAlert("Borrar Cuenta", "ESTA ACCIÓN ES IRREVERSIBLE" +
					"\n\n¿Está realmente seguro de que quiere borrar su cuenta?", "No", "Sí"))
				{
					if (await App.restService.DeleteUsuario(new Usuario() { id = App.loggedUser }))
						OnLogout();
					else
						await DisplayAlert("Alerta", "Se produjo un error al intentar borrar la cuenta.", "Aceptar");
				}
			}
		}

		private void OnLogout()
		{
			App.loggedUser = -1;
			File.Delete(Path.Combine(App.Path, "user"));
			Application.Current.MainPage = new AppShell();
		}

		private async void ChangeVoice(object sender, EventArgs e)
		{
			string response = await DisplayActionSheet("Género", "Cancelar", null, "Masculino", "Femenino");

			if(!String.Equals(response, "Cancelar"))
			{
				if(String.Equals(response, "Masculino"))
				{
					App.speechService.Voice = SpeechService.MaleVoice;
				}
				else
				{
					App.speechService.Voice = SpeechService.FemaleVoice;
				}
				File.WriteAllText(Path.Combine(App.Path, "voice"), App.speechService.Voice);
			}
		}

	}
}
