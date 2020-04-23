using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class SitesPage : BasePage<SitesViewModel>
    {
        public SitesPage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
        }
    }
}
