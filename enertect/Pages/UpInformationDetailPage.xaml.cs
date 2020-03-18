using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class UpInformationDetailPage : BasePage<UpInformationDetailViewModel>
    {
        public UpInformationDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            var viewModel = (UpInformationDetailViewModel)this.DataContext;
            viewModel.UpdateData();
            this.Chart.BindingContext = viewModel;
        }
    }
}
