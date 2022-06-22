using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BackButtonView : ContentView
	{
		public BackButtonView()
		{
			InitializeComponent();
		}

		async void OnGoBack(object sender, EventArgs e)
		{
			Button.TextColor = Color.Accent;
			await Navigation.PopAsync();
		}
	}
}
