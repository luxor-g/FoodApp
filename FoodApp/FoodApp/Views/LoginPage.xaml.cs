using FoodApp.ViewModels;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			this.BindingContext = new LoginViewModel();
			PasswordView.Entry.TextChanged += OnTextChanged;
		}

		async void GoToNewPassword(object sender, EventArgs args) => await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		async void GoToRegister(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new RegisterPage());
			Navigation.RemovePage(this);
		}

		void OnTextChanged(object sender, EventArgs args)
		{
			if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordView.Entry.Text))
			{
				LoginButton.IsEnabled = false;
			}
			else
			{
				LoginButton.IsEnabled = new Regex("^\\S+@\\S+\\.\\S+$").IsMatch(EmailEntry.Text)
					&& new Regex("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*[0-9]).*$").IsMatch(PasswordView.Entry.Text);
			}
		}
	}
}
