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
