using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Data.Models;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace enertect.Core.ViewModels
{
    public class SitesViewModel : BaseViewModel
    {
        readonly IApiService _apiService;

        public SitesViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            _apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            var user_pre = Preferences.Get(AppConstant.USER_TOKEN, "");
            User user = JsonConvert.DeserializeObject<User>(user_pre);
            if(user != null)
            {
                _sites = new ObservableCollection<SiteItemViewModel>(user.SitesEndPoints.Select(v => v.ToSiteItemViewModel()));
            }
            

        }

        public override void Prepare()
        {
        }

        #region Properties
        private ObservableCollection<SiteItemViewModel> _sites = new ObservableCollection<SiteItemViewModel>();
        public ObservableCollection<SiteItemViewModel> Sites
        {
            get
            {
                return _sites;
            }
            set
            {
                SetProperty(ref _sites, value);
            }
        }
        #endregion

        #region Commands

        public IMvxAsyncCommand<SiteItemViewModel> TapItemCommand => new MvxAsyncCommand<SiteItemViewModel>(GoToHome);

        async Task GoToHome(SiteItemViewModel vmItem)
        {
            Preferences.Set(AppConstant.SITE_URL, vmItem.SiteUrl);
            await _navigationService.Navigate<HomePageViewModel>();

        }

        public IMvxAsyncCommand SignOutCommand => new MvxAsyncCommand(SignOut);

        private async Task SignOut()
        {
            Preferences.Set(AppConstant.USER_TOKEN, "");
            Preferences.Set(AppConstant.SITE_URL, "");

            await ClearStackAndNavigateToPage<SignInViewModel>();
        }

        #endregion
    }
}
