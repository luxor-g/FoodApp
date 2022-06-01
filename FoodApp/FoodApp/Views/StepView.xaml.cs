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
	public partial class StepView : ContentView
	{
		const string CircledNumbers = "❶❷❸❹❺❻❼❽❾❿";

		public StepView()
		{
			InitializeComponent();
		}

		public StepView(int number, string description)
		{
			InitializeComponent();
			Description.Text = description;

			if (number < 0 || number > 9)
				Number.Text = Char.ToString(CircledNumbers[0]);
			else
				Number.Text = Char.ToString(CircledNumbers[number]);
		}
	}
}
