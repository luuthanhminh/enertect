using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class UpInformationDetailPage : BasePage<UpInformationDetailViewModel>, ICallsView
    {
        public UpInformationDetailPage()
        {
            InitializeComponent();
        }

        public void BindingChart(UpInformationDetailViewModel viewModel)
        {
            if(this.Chart != null)
            {
                this.Chart.BindingContext = viewModel;
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.ViewModel.View = this;

            //var viewModel = (UpInformationDetailViewModel)this.DataContext;
            //this.Chart.BindingContext = viewModel;
        }
    }
}
