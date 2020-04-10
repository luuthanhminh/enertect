using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class HomePage : BasePage<HomePageViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
        }

        void TabularGrid_QueryRowHeight(System.Object sender, Syncfusion.SfDataGrid.XForms.QueryRowHeightEventArgs e)
        {
            if (e.RowIndex != 0)
            {
                //Calculates and sets height of the row based on its content 
                e.Height = (sender as SfDataGrid).GetRowHeight(e.RowIndex);
                e.Handled = true;
            }
        }
    }
}
