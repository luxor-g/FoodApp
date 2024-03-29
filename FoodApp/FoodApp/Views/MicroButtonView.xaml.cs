using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MicroButtonView : ContentView
	{
		public MicroButtonView()
		{
			InitializeComponent();
		}

		private async void OnClicked(object sender, EventArgs e)
		{
			Button.IsEnabled = false;
			Button.TextColor = Color.Accent;

			await Shell.Current.GoToAsync(nameof(AssistantPage));
			await Task.Delay(500);

			Button.IsEnabled = true;
			Button.TextColor = Color.White;
			Button.BackgroundColor = Color.Transparent;
		}
	}
}
