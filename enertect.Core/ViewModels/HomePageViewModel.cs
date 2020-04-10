using System;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Essentials;

namespace enertect.Core.ViewModels
{
    public class HomePageViewModel: BaseViewModel
    {
        readonly IApiService _apiService;

        public HomePageViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            _apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            GetHomeInfo();
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
        #endregion

        #region Methods

        async Task GetHomeInfo()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var res = await _apiService.getHomepageInfo();

                    if (res.IsSuccess)
                    {
                        
                    }
                    else
                    {
                        await _dialogService.ShowMessage("Error", String.Join(Environment.NewLine, res.Errors), "Close");
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

        }

        #endregion

        #region Commands

        public IMvxAsyncCommand TapHistoryCommand => new MvxAsyncCommand(GoToHistory);

        async Task GoToHistory()
        {
            //await _navigationService.Navigate<HistoryUpInformationViewModel, UpsItemViewModel>(ItemViewModel);
        }

        #endregion
    }
}
