using FoodApp.Services;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssistantPage : ContentPage
	{
		public AssistantPage()
		{
			InitializeComponent();
			
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			//await App.speechService.TextToSpeech("¿Cómo puedo ayudarte?");
			var result = await App.speechService.SpeechToText();

			label.Text = result.Text;
			var cancellation = CancellationDetails.FromResult(result);
			Debug.WriteLine(cancellation);

		}
	}
}
