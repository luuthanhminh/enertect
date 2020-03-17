using System;
using enertect.iOS.Presenter;
using enertect.UI;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platforms.Ios.Presenters;

namespace enertect.iOS
{
    public class Setup : MvxFormsIosSetup<CoreApp, App>
    {
        protected override IMvxIosViewPresenter CreateViewPresenter()
        {
            var presenter = new IosCustomPresenter(this.ApplicationDelegate, this.Window, this.FormsApplication);
            Mvx.IoCProvider.RegisterSingleton<IMvxFormsViewPresenter>(presenter);

            return presenter;
        }
    }
}
