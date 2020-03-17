using System;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace enertect.UI.Pages.Base
{
    public class BasePage<TViewModel> : MvxContentPage<TViewModel> where TViewModel : MvxViewModel
    {
        public BasePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
