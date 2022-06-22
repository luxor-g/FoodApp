using FoodApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipePage : ContentPage
	{
		public RecipePage()
		{
			InitializeComponent();
			this.BindingContext = new RecipeViewModel();
			Shell.SetTabBarIsVisible(this, false);
		}

		protected override async void OnDisappearing()
		{
			base.OnDisappearing();
			await Task.Delay(500);
			Navigation.RemovePage(this);
		}
	}
}
