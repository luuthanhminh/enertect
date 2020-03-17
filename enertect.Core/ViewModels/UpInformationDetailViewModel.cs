using System;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace enertect.Core.ViewModels
{
    public class UpInformationDetailViewModel : BaseWithObjectViewModel<UpsItemViewModel>
    {
        readonly IApiService _apiService;
        UpsItemViewModel _upsItemViewModel;

        public UpInformationDetailViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            this._apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

        }

        public override void Prepare(UpsItemViewModel parameter)
        {
            _upsItemViewModel = parameter;
            this.UpsName = _upsItemViewModel.UpsName;
        }

        #region Properties

        private string _upsName;
        public string UpsName
        {
            get
            {
                return _upsName;
            }
            set
            {
                SetProperty(ref _upsName, value);
            }
        }

        #endregion

        #region Commands

       

        #endregion
    }
}
