using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeView : ContentView
	{
		public RecipeView()
		{
			InitializeComponent();
		}

		public RecipeView(string type, string time, bool hearted, string hearts)
		{
			InitializeComponent();

			Type.Text = type;
			Time.Text = time + " min";
			HeartIcon.Text = (hearted) ? "" : "";
			HeartsCount.Text = hearts;
		}

		async void OnClicked(object sender, EventArgs args)
		{
			await Shell.Current.GoToAsync(nameof(RecipePage));
		}
	}
}
