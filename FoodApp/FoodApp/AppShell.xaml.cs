using FoodApp.Views;
using Xamarin.Forms;

namespace FoodApp
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));


			Routing.RegisterRoute(nameof(RecipePage), typeof(RecipePage));
		}

	}
}
