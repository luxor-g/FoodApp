using System;
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
	}
}
