using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using enertect.UI;
using MvvmCross.Forms.Platforms.Android.Views;

namespace enertect.Droid
{

    [Activity(
         Label = "Enertect"
         , MainLauncher = true
         , Icon = "@mipmap/icon"
         , Theme = "@style/Theme.Splash"
         , NoHistory = true
         , LaunchMode = LaunchMode.SingleTask
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Locale | ConfigChanges.LayoutDirection
         , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity: MvxFormsSplashScreenAppCompatActivity<Setup, CoreApp, App>
    {
        public SplashActivity() : base(Resource.Layout.SplashScreen)
        {

        }

        protected override void OnCreate(Bundle bundle)
        {
            global::Xamarin.Forms.Forms.Init(this, bundle);


            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.Fullscreen);


        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return Task.CompletedTask;
        }
    }
}
