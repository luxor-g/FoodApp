using System;
using System.Text.RegularExpressions;
using FoodApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		RegisterViewModel rvm;
		public RegisterPage()
		{
			InitializeComponent();
			rvm = new RegisterViewModel();
			this.BindingContext = rvm;

			PasswordView.Entry.TextChanged += OnTextChanged;
			PasswordView.Entry.ReturnType = ReturnType.Next;

			RepeatPasswordView.Entry.Placeholder = "Repetir Contraseña";
			RepeatPasswordView.Entry.TextChanged += OnTextChanged;

			RegisterButton.IsEnabled = false;
		}

		async void GoToLogin(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new LoginPage());
			Navigation.RemovePage(this);
		}

		bool IsTextValid()
		{
			if (string.IsNullOrEmpty(NameEntry.Text) || string.IsNullOrEmpty(EmailEntry.Text)
				|| string.IsNullOrEmpty(PasswordView.Entry.Text) || string.IsNullOrEmpty(RepeatPasswordView.Entry.Text))
			{
				return false;
			}
			else
			{
				return new Regex("^\\S+@\\S+\\.\\S+$", RegexOptions.Compiled).IsMatch(EmailEntry.Text)
					&& new Regex("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*[0-9]).*$", RegexOptions.Compiled).IsMatch(PasswordView.Entry.Text)
					&& string.Equals(PasswordView.Entry.Text, RepeatPasswordView.Entry.Text);
			}
		}

		void OnTextChanged(object sender, EventArgs args)
		{
			RegisterButton.IsEnabled = IsTextValid();
			rvm.Clave = PasswordView.Entry.Text;
		}
	}
}
