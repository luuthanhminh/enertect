using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class AlarmsViewPage : BasePage<AlarmsViewModel>
    {
        public AlarmsViewPage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
        }
    }
}
