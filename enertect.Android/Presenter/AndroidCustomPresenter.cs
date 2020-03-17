using System;
using System.Collections.Generic;
using System.Reflection;
using enertect.UI.Presenter;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Presenters;
using MvvmCross.Forms.Presenters;
using Xamarin.Forms;

namespace enertect.Droid.Presenter
{
    public class AndroidCustomPresenter : MvxFormsAndroidViewPresenter
    {
        public AndroidCustomPresenter(IEnumerable<Assembly> androidViewAssemblies, Application formsApplication) : base(androidViewAssemblies, formsApplication)
        {
        }

        private IMvxFormsPagePresenter _formsPagePresenter;
        public override IMvxFormsPagePresenter FormsPagePresenter
        {
            get
            {
                if (_formsPagePresenter == null)
                {
                    _formsPagePresenter = new AppCustomPresenter(this);
                    Mvx.IoCProvider.RegisterSingleton(_formsPagePresenter);
                }
                return _formsPagePresenter;
            }
            set
            {
                _formsPagePresenter = value;
            }
        }
    }
}
