using System;
using System.Collections.Generic;
using enertect.Core.Data.DialogViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace enertect.UI.Controls.Popups
{
    public partial class SimpleDialogNoTitle : PopupPage
    {
        public SimpleDialogNoTitle(DialogViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
