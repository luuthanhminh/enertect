using System;
using System.Collections.Generic;
using enertect.Core.Data.DialogViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace enertect.UI.Controls.Popups
{
    public partial class CustomActionSheet : PopupPage
    {
        DialogViewModel _viewModel
        {
            get
            {
                return this.BindingContext as DialogViewModel;
            }
        }

        DialogAction _selectedItem = null;
        public CustomActionSheet(DialogViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
            this.lvActions.ItemTapped += LvActions_ItemTapped;
            PopupNavigation.Instance.Popped += Instance_Popped;

        }

        private void Instance_Popped(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
        {
            if (_viewModel.CompletionSource != null)
            {
                if (_selectedItem != null)
                {
                    _viewModel.CompletionSource.TrySetResult(_selectedItem.Label);
                }
                else
                {
                    _viewModel.CompletionSource.TrySetResult("Cancel");
                }

            }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();


            this.lvActions.ItemTapped -= LvActions_ItemTapped;
        }

        private void LvActions_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData is DialogAction dialogAction)
            {
                if (_viewModel.CompletionSource != null)
                {
                    _selectedItem = dialogAction;
                    PopupNavigation.Instance.PopAsync();
                }
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
