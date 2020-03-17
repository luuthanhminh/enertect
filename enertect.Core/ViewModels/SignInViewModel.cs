using System;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Navigation;

namespace enertect.Core.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {

        public SignInViewModel(IMvxNavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            this.UserName = "engineer1.test";
            this.Password = "test11203";
        }

        #region Properties
        private String _username;
        public String UserName
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

        private String _password;
        public String Password
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

        #region Methods

        #endregion
    }
}
