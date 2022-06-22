using FoodApp.Models;
using FoodApp.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FoodApp
{
	public partial class App : Application
	{
		public static RestService restService;
		public static SpeechService speechService;

		public static int loggedUser;
		public static string Path;

		public static List<Receta> Recetas;

		public App()
		{
			InitializeComponent();

			restService = new RestService();
			speechService = new SpeechService();
			Path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
