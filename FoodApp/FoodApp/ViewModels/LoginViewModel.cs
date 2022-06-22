using FoodApp.Models;
using FoodApp.Views;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private string email;
		private string clave;

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

		public Command LoginCommand { get; }

		public LoginViewModel()
		{
			LoginCommand = new Command(OnLoginClicked);
		}

		private async void OnLoginClicked(object sender)
		{
			await Login(new Usuario()
			{
				email = Email,
				clave = Clave
			});
		}

		public static async Task Login(Usuario usuario)
		{
			int id = await App.restService.Login(usuario);
			if (id > 0)
			{
				App.loggedUser = id;
				File.WriteAllText(Path.Combine(App.Path, "user"), id.ToString());
				if (await App.restService.GetRecetasAsUsuario(new Usuario { id = App.loggedUser }))
					await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
				else
					await Application.Current.MainPage.DisplayAlert("Alerta", "Ocurrió un problema inesperado al intentar " +
						"cargar las recetas, por favor vuelva a intentarlo más tarde.", "Aceptar");
			}
			else
				await Application.Current.MainPage.DisplayAlert("Alerta", "Email y/o contraseña incorrectos.", "Aceptar");
		}
	}
}
