using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace enertect
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjM1MTA0QDMxMzgyZTMxMmUzMFpDd2YwTkxabkhyS2ROSVdwWjVCV3Rta3NOeS84aStwM2tZdlJvWWhUSE09");
            InitializeComponent();

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
