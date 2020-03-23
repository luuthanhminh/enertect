using System;
using System.Collections.Generic;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class UpInformationDetailPage : BasePage<UpInformationDetailViewModel>, ICallsView
    {
        public UpInformationDetailPage()
        {
            InitializeComponent();
        }

        public void BindingChart()
        {
            if(this.Chart != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    Color color = Color.FromHex("#F3584F");
                    string value = "Voltage";
                    switch (i)
                    {
                        case 1:
                            color = Color.FromHex("#00bdae");
                            value = "Resitance";
                            break;
                        case 2:
                            color = Color.FromHex("#98C862");
                            value = "Temperature";
                            break;
                    }

                    FastLineSeries columnSeries = new FastLineSeries()
                    {
                        ItemsSource = ViewModel.Ups,
                        XBindingPath = "StringName",
                        YBindingPath = value,
                        Color = color,
                        StrokeWidth = 1,
                        Label = value,
                        LegendIcon = ChartLegendIcon.SeriesType,
                        DataMarker = new ChartDataMarker()
                        {
                            ShowMarker = true,
                            ShowLabel = false,
                            MarkerBorderColor = color,
                            MarkerBorderWidth = 0,
                            MarkerColor = color,
                            MarkerWidth = 3,
                            MarkerHeight = 3
                        }
                    };

                    this.Chart.Series.Add(columnSeries);
                }

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
