using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;

namespace enertect.Core.ViewModels.Base
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        protected readonly IDialogService _dialogService;

        #region Constructors

        public BaseViewModel(IMvxNavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        #endregion

        #region Binding Properties

        #region IsLoading

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        #endregion

        #endregion

        #region Methods

        protected void PopToPage<TViewModel>() where TViewModel : MvxViewModel
        {
            var hint = new MvxPopPresentationHint(typeof(TViewModel));
            _navigationService.ChangePresentation(hint);
        }

        protected async Task ClearStackAndNavigateToPage<TViewModel>() where TViewModel : MvxViewModel
        {
            var presentation = new MvxBundle(new Dictionary<string, string> { { PresentationConstant.CLEAR_STACK_AND_SHOW_PAGE, "" } });

            await _navigationService.Navigate<TViewModel>(presentationBundle: presentation);
        }

        protected void ShowLoading()
        {
            IsLoading = true;
        }

        protected void HideLoading()
        {
            IsLoading = false;
        }

        #endregion

        #region Commands

        public IMvxAsyncCommand CloseCommand => new MvxAsyncCommand(ClosePage);

        protected async Task ClosePage()
        {
            await _navigationService.Close(this);
        }

        #endregion
    }
}
