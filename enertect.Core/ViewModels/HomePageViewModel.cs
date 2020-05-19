using System;
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
            var user_pre = Preferences.Get(AppConstant.USER_TOKEN, "");
            User user = JsonConvert.DeserializeObject<User>(user_pre);
            this.IsSiteHome = user.SitesEndPoints.Count > 1;
            this.Title = this.IsSiteHome ? "Sites" : "Back";
            GetHomeInfo();
        }

        #region Properties

        public bool IsSiteHome { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        private OverviewItemViewModel _overview;
        public OverviewItemViewModel Overview
        {
            get
            {
                return _overview;
            }
            set
            {
                SetProperty(ref _overview, value);
            }
        }

        private ObservableCollection<AlarmItemViewModel> _criticalAlarmsList = new ObservableCollection<AlarmItemViewModel>();
        public ObservableCollection<AlarmItemViewModel> CriticalAlarmsList
        {
            get
            {
                return _criticalAlarmsList;
            }
            set
            {
                SetProperty(ref _criticalAlarmsList, value);
            }
        }

        private AlarmsOverViewModel _alarmsOverView;
        public AlarmsOverViewModel AlarmsOverView
        {
            get
            {
                return _alarmsOverView;
            }
            set
            {
                SetProperty(ref _alarmsOverView, value);
            }
        }

        private ObservableCollection<UpsReadingItemViewModel> _upsReadingsList = new ObservableCollection<UpsReadingItemViewModel>();
        public ObservableCollection<UpsReadingItemViewModel> UpsReadingsList
        {
            get
            {
                return _upsReadingsList;
            }
            set
            {
                SetProperty(ref _upsReadingsList, value);
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
                        Overview = res.ResponseObject.UpsOverview.ToOverviewModel();
                        AlarmsOverView = res.ResponseObject.AlarmsOverView.ToAlarmsOverViewModel();
                        CriticalAlarmsList = new ObservableCollection<AlarmItemViewModel>(res.ResponseObject.CriticalAlarmsList.Select(v => v.ToAlarmItemViewModel(null)));
                        UpsReadingsList = new ObservableCollection<UpsReadingItemViewModel>(res.ResponseObject.UpsReadingsList.Select(v => v.ToUpsReadingItemViewModel()));
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

        public IMvxAsyncCommand SignOutCommand => new MvxAsyncCommand(SignOut);

        private async Task SignOut()
        {
            Preferences.Set(AppConstant.USER_TOKEN, "");

            await ClearStackAndNavigateToPage<SitesViewModel>();
        }

        public IMvxAsyncCommand AlarmDetailCommand => new MvxAsyncCommand(GoToAlarm);

        private async Task GoToAlarm()
        {
            await _navigationService.Navigate<AlarmsViewModel>();
        }

        public IMvxAsyncCommand TapGotoUpsCommand => new MvxAsyncCommand(GotoUps);

        private async Task GotoUps()
        {
            await _navigationService.Navigate<UpsInformationViewModel>();
        }

        public IMvxAsyncCommand TapHomeCommand => new MvxAsyncCommand(GoToHome);

        private async Task GoToHome()
        {
            if (this.IsSiteHome)
            {
                this.CloseCommand.Execute();
            }
            else
            {
                Preferences.Set(AppConstant.USER_TOKEN, "");
                await ClearStackAndNavigateToPage<SignInViewModel>();
            }  
        }

        #endregion
    }
}
