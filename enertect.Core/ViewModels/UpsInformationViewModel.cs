using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Essentials;

namespace enertect.Core.ViewModels
{
    public class UpsInformationViewModel : BaseViewModel
    {
        readonly IApiService _apiService;

        public UpsInformationViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            this._apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            _siteName = "";
            LoadData();
            GetSiteName();
        }

        #region Properties

        private string _siteName;
        public string SiteName
        {
            get
            {
                return _siteName;
            }
            set
            {
                SetProperty(ref _siteName, value);
            }
        }

        private ObservableCollection<UpsItemViewModel> _ups = new ObservableCollection<UpsItemViewModel>();
        public ObservableCollection<UpsItemViewModel> Ups
        {
            get
            {
                return _ups;
            }
            set
            {
                SetProperty(ref _ups, value);
            }
        }

        private bool _hasNoData;
        public bool HasNoData
        {
            get
            {
                return _hasNoData;
            }
            set
            {
                SetProperty(ref _hasNoData, value);
            }
        }

        #endregion

        #region Methods

        async Task LoadData()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    HasNoData = false;
                    this.Ups.Clear();
                    ShowLoading();

                    var res = await _apiService.getUpsInfornations();

                    if (res.IsSuccess)
                    {
                        var ups = res.ResponseListObject;
                        this.Ups = new ObservableCollection<UpsItemViewModel>(ups.Select(v => v.ToItemViewModel()));
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
                HasNoData = this.Ups.Count == 0;

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

        async Task GetSiteName()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var res = await _apiService.getSiteDetail();

                    if (res.IsSuccess)
                    {
                        SiteName = res.ResponseObject.SiteName;
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

            await ClearStackAndNavigateToPage<SignInViewModel>();
        }

        public IMvxAsyncCommand<UpsItemViewModel> TapItemCommand => new MvxAsyncCommand<UpsItemViewModel>(GoToDetail);

        async Task GoToDetail(UpsItemViewModel vmItem)
        {
            if(vmItem.Items != null)
            {
                if (vmItem.Items.Any()  && vmItem.Items.Count > 1)
                {
                    await _navigationService.Navigate<UpsStringViewModel, UpsItemViewModel>(vmItem);
                }
                else
                {
                    await _navigationService.Navigate<UpInformationDetailViewModel, UpsItemViewModel>(vmItem.Items.First());
                }

            }
            

        }

        public IMvxAsyncCommand AlarmDetailCommand => new MvxAsyncCommand(GoToAlarm);

        private async Task GoToAlarm()
        {
            var ViewModel = new UpsViewModel() { Ups = this.Ups };
            await _navigationService.Navigate<AlarmsViewModel>();
        }

        #endregion

    }
}
