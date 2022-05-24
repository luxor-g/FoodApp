using FoodApp.Models;
using FoodApp.ViewModels;
using Xamarin.Forms;

namespace FoodApp.Views
{
	public partial class NewItemPage : ContentPage
	{
		public Item Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();
			BindingContext = new NewItemViewModel();
		}
	}
}
