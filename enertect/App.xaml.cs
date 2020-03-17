using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace enertect
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjEzNDAzQDMxMzcyZTM0MmUzMFpETXVMYlNpMTEwa0dCSkhydjFjTzBWWjZTZStUTkFMclB2aXFLaWZvWGs9");
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
