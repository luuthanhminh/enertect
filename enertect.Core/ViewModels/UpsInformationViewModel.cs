using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
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

            LoadData();
        }

        #region Properties
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

        #endregion

        #region Methods

        async Task LoadData()
        {
            try
            {

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Connection to internet is available

                    this.Ups.Clear();
                    ShowLoading();

                    var res = await _apiService.getUpsInfornations();

                    if (res.IsSuccess)
                    {
                        // Update local db
                        //await _jobCallRepository.AddOrUpdateItems(res.ResponseObject.Select(v => v.ToDTO()).ToList());

                        //var jobs = _jobCallRepository.GetAll();

                        //this.Ups = new ObservableCollection<UpsItemViewModel>(jobs.Select(v => v.ToItemViewModel(this)));
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
            finally
            {
                HideLoading();
            }

        }
        #endregion
    }
}
