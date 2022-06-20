using FoodApp.Models;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
	public class RegisterViewModel : BaseViewModel
	{
		private string nombre;
		private string email;
		private string clave;
		public string Nombre
		{
			get => nombre;
			set => SetProperty(ref nombre, value);
		}
		public string Email
		{
			get => email;
			set => SetProperty(ref email, value);
		}
		public string Clave
		{
			get => clave;
			set => SetProperty(ref clave, value);
		}

		public Command RegisterCommand { get; }

		public RegisterViewModel()
		{
			RegisterCommand = new Command(OnSave);
		}

		private async void OnSave()
		{
			Usuario newUsuario = new Usuario()
			{
				nombre = Nombre,
				email = Email,
				clave = Clave
			};

			if (await App.restService.CreateUsuario(newUsuario))
				await LoginViewModel.Login(newUsuario);
			else
				await Application.Current.MainPage.DisplayAlert("Alerta", "El email introducido ya se encuentra en uso.", "Aceptar");
		}

	}
}
