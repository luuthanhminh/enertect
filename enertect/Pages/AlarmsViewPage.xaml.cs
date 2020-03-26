using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class AlarmsViewPage : BasePage<AlarmsViewModel>, ICallsAlarmsView
    {
        public AlarmsViewPage()
        {
            InitializeComponent();
        }

        public void ShowResolvedDate()
        {
            DoBResolvedPicker.IsVisible |= Device.RuntimePlatform == Device.Android;
            Device.BeginInvokeOnMainThread(() =>
            {
                DoBResolvedPicker.Focus();
                DoBResolvedPicker.IsVisible = false;
            });
        }

        public void ShowAlarmDate()
        {
            DoBAlarmPicker.IsVisible |= Device.RuntimePlatform == Device.Android;
            Device.BeginInvokeOnMainThread(() =>
            {
                DoBAlarmPicker.Focus();
                DoBAlarmPicker.IsVisible = false;
            });
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.ViewModel.View = this;
        }
    }
}
