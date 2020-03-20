using System;
using System.Threading.Tasks;
using enertect.Core.Data.Models;
using enertect.Core.Helpers;
using enertect.Core.Services;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels;
using enertect.UI.Services;
using Flurl.Http;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace enertect.UI
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            var user_pre= Preferences.Get(AppConstant.USER_TOKEN, "");
            User user = JsonConvert.DeserializeObject<User>(user_pre);
            if (user == null)
            {
                RegisterCustomAppStart<CustomMvxAppStart<SignInViewModel>>();
            }
            else
            {
                RegisterCustomAppStart<CustomMvxAppStart<UpsInformationViewModel>>();
            }

            FlurlHttp.Configure(settings => settings.Timeout = TimeSpan.FromSeconds(20));

            #region IOC

            Mvx.IoCProvider.RegisterType<IDialogService, DialogService>();
            Mvx.IoCProvider.RegisterType<IApiService, ApiService>();

            #endregion


        }
    }

    public class CustomMvxAppStart<TViewModel> : MvxAppStart<TViewModel>
     where TViewModel : IMvxViewModel
    {
        public CustomMvxAppStart(IMvxApplication application, IMvxNavigationService navigationService) : base(application, navigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<TViewModel>();
        }

    }
}
