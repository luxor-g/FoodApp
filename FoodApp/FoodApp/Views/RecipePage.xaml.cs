using FoodApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipePage : ContentPage
	{
		public RecipePage()
		{
			this.BindingContext = new RecipeViewModel();
			InitializeComponent();
		}


	}
}
