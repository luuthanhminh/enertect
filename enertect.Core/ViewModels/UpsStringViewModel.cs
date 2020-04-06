using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace enertect.Core.ViewModels
{
    public class UpsStringViewModel : BaseWithObjectViewModel<UpsItemViewModel>
    {
        readonly IApiService _apiService;

        public UpsStringViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            _apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public override void Prepare(UpsItemViewModel parameter)
        {
            _itemViewModel = parameter;
            _upsName = $"{parameter.UpsName}";
            _ups = parameter.Items;
        }

        #region Properties
        private UpsItemViewModel _itemViewModel;
        public UpsItemViewModel ItemViewModel
        {
            get
            {
                return _itemViewModel;
            }
            set
            {
                SetProperty(ref _itemViewModel, value);
            }
        }

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

        #region Commands

        public IMvxAsyncCommand<UpsItemViewModel> TapItemCommand => new MvxAsyncCommand<UpsItemViewModel>(GoToDetail);

        async Task GoToDetail(UpsItemViewModel vmItem)
        {
            await _navigationService.Navigate<UpInformationDetailViewModel, UpsItemViewModel>(vmItem);

        }

        #endregion
    }
}
