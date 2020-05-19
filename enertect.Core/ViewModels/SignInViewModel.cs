using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace enertect.Core.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        readonly IApiService _apiService;
        public SignInViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            _apiService = apiService;
            //this.UserName = "admin@123";
            //this.Password = "admin";
        }

        #region Properties
        private string _username;
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        #endregion

        #region Commands

        public IMvxAsyncCommand SignInCommand => new MvxAsyncCommand(SignIn);

        private async Task SignIn()
        {
            await SignInWithUserName();
        }

        #endregion

        #region Methods

        async Task SignInWithUserName()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if (this.UserName.Length != 0 && this.Password.Length != 0)
                    {
                        ShowLoading();

                        var res = await _apiService.SignIn(this.UserName, this.Password);

                        if (res.IsSuccess)
                        {
                            var user = res.ResponseObject;
                            Preferences.Set(AppConstant.USER_TOKEN, JsonConvert.SerializeObject(user));
                            if(user.SitesEndPoints.Count > 1)
                            {
                                await _navigationService.Navigate<SitesViewModel>();
                            }
                            else if (user.SitesEndPoints.Count == 1)
                            {
                                Preferences.Set(AppConstant.SITE_URL, user.SitesEndPoints.First().SiteUrl);
                                await ClearStackAndNavigateToPage<HomePageViewModel>();//UpsInformationViewModel
                            }
                            
                        }
                        else
                        {
                            await _dialogService.ShowMessage("Error", String.Join(Environment.NewLine, res.Errors), "Close");
                        }
                    }
                    else
                    {
                        await _dialogService.ShowMessage("Error", "Username and password required!", "Close");
                    }
                }
                else
                {

                    await _dialogService.ShowMessage("Error", "No internet access", "Close");
                }

            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessage("Error", ex.Message, "Close");
            }
            finally
            {
                HideLoading();
            }

        }

        #endregion
    }
}
