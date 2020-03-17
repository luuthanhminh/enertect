using System;
using enertect.Droid.Presenter;
using enertect.UI;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platforms.Android.Presenters;

namespace enertect.Droid
{
    public class Setup : MvxFormsAndroidSetup<CoreApp, App>
    {
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new AndroidCustomPresenter(this.GetViewAssemblies(), this.FormsApplication);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsViewPresenter>(presenter);
            return presenter;
        }
    }
}
