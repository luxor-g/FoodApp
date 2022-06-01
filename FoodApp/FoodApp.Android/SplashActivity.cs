using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
using Android.Util;
using AndroidX.AppCompat.App;

namespace FoodApp.Droid
{
	[Activity(Label = "FoodApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : Activity
	{
		static readonly string TAG = "X:" + typeof(SplashActivity).Name;

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
			base.OnCreate(savedInstanceState, persistentState);
			Log.Debug(TAG, "SplashActivity.OnCreate");
		}

		// Launches the startup task
		protected override void OnResume()
		{
			base.OnResume();
			Task startupWork = new Task(() => { SimulateStartup(); });
			startupWork.Start();
		}

		// Prevent the back button from canceling the startup process
		public override void OnBackPressed() { }

		// Simulates background work that happens behind the splash screen
		async void SimulateStartup()
		{
			Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
			//await Task.Delay(50); // Simulate a bit of startup work.
			Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
			StartActivity(new Intent(Application.Context, typeof(MainActivity)));
			OverridePendingTransition(Resource.Animation.nav_default_pop_enter_anim, Resource.Animation.nav_default_pop_exit_anim);
		}
	}
}
