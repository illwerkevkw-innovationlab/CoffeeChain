﻿using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace CoffeeChain.App.Droid
{
    [Activity(Theme = "@style/MainTheme.Startup", MainLauncher = true, NoHistory = true)]
    public class StartupActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(StartupActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();

            Task.Run(() => SimulateStartup());
        }

        // Simulates background work that happens behind the splash screen
        private async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            //await Task.Delay(1000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
