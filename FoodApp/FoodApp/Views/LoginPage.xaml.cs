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
		readonly LoginViewModel lvm;
		public LoginPage()
		{
			InitializeComponent();
			lvm = new LoginViewModel();
			this.BindingContext = lvm;
			PasswordView.Entry.TextChanged += OnTextChanged;
			LoginButton.IsEnabled = false;
		}

		void GoToNewPassword(object sender, EventArgs args)
		{
			//await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
		}

		async void GoToRegister(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new RegisterPage());
			Navigation.RemovePage(this);
		}

		bool IsTextValid()
		{
			if (string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordView.Entry.Text))
			{
				return false;
			}
			else
			{
				return new Regex("^\\S+@\\S+\\.\\S+$", RegexOptions.Compiled).IsMatch(EmailEntry.Text)
					&& new Regex("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*[0-9]).*$", RegexOptions.Compiled).IsMatch(PasswordView.Entry.Text);
			}
		}

		void OnTextChanged(object sender, EventArgs args)
		{
			LoginButton.IsEnabled = IsTextValid();
			lvm.Clave = PasswordView.Entry.Text;
		}
	}
}
