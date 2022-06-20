using FoodApp.Views;
using Xamarin.Forms;

namespace FoodApp
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			Routing.RegisterRoute(nameof(StartUpPage), typeof(StartUpPage));
			Routing.RegisterRoute(nameof(RecipePage), typeof(RecipePage));
			Routing.RegisterRoute(nameof(AssistantPage), typeof(AssistantPage));

			InitializeComponent();
		}

	}
}
