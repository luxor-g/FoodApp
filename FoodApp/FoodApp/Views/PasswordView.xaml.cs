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
	public partial class PasswordView : ContentView
	{
		public PasswordView()
		{
			InitializeComponent();
		}

		void OnButtonClicked(object sender, EventArgs args)
		{
			int position = Entry.CursorPosition;

			Entry.IsPassword = !Entry.IsPassword;
			Button.TextColor = (Entry.IsPassword) ? Color.White : Color.Accent;

			if (Entry.IsFocused)
				Entry.CursorPosition = position;
		}
	}
}
