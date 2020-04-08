using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Data.Models.Ups;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using enertect.UI.Services.Interfaces;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using Xamarin.Forms;

namespace enertect.UI.Pages
{
    public partial class HistoryUpInformationPage : BasePage<HistoryUpInformationViewModel>, ICallsViewHistory
    {
        public HistoryUpInformationPage()
        {
            InitializeComponent();
        }

        public void BindingChart(IList<UpsItemViewModel> datas)
        {
            ChartColorCollection colors = (ChartColorCollection)App.Current.Resources["ChartColors"];
            RenderChart(VolChart, datas, colors, "Voltage", 13.6, 13.4, 0.02);
            RenderChart(ResChart, datas, colors, "Resitance", 7, 0, 1);
            RenderChart(TempChart, datas, colors, "Temperature", 29, 20, 1);
        }

        void RenderChart(SfChart chart, IList<UpsItemViewModel> datas, ChartColorCollection colors, string value,
            double max, double min, double interval)
        {
            if (chart != null)
            {
                chart.HeightRequest = 400;
                chart.Series.Clear();
                for (int i = 0; i < datas.Count; i++)
                {
                    Color color = colors[i];
                    //string value = "Voltage";
                    FastLineSeries columnSeries = new FastLineSeries()
                    {
                        ItemsSource = datas[i].UpsHistoryTrendings,
                        XBindingPath = "DateValue",
                        YBindingPath = value,
                        Color = color,
                        StrokeWidth = 1,
                        Label = i.ToString(),
                        LegendIcon = ChartLegendIcon.Rectangle,
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
                    chart.PrimaryAxis = new CategoryAxis()
                    {
                        LabelPlacement = LabelPlacement.BetweenTicks,
                        ShowMajorGridLines = false
                    };
                    chart.SecondaryAxis = new NumericalAxis()
                    {
                        Maximum = max,
                        Minimum = min,
                        Interval = interval,
                        AxisLineStyle = new ChartLineStyle()
                        {
                            StrokeWidth = 0
                        },
                        MajorTickStyle = new ChartAxisTickStyle()
                        {
                            TickSize = 0
                        }
                    };
                    chart.Legend = new ChartLegend()
                    {
                        OverflowMode = ChartLegendOverflowMode.Wrap,
                        DockPosition = LegendPlacement.Top,
                        IconHeight = 10,
                        IconWidth = 10,
                        ToggleSeriesVisibility = true
                    };
                    chart.Series.Add(columnSeries);
                }

            }
        }

        public async Task<bool> ExportExcel()
        {
            DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
            var excelEngine = excelExport.ExportToExcel(this.TabularGrid);
            var workbook = excelEngine.Excel.Workbooks[0];
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();
            excelEngine.Dispose();
            var fileName = $"{lbTitle.Text.ToLower().Replace(" ", "_").Replace(",", "")}_{DateTime.Now.ToString("HH_mm_MMM_dd_yyyy")}.xlsx";
            return await DependencyService.Get<IExportToExcelService>().ExportAsExcel(fileName, "application/msexcel", stream);
        }

        public void ExportPDF()
        {

        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.ViewModel.View = this;
        }

        public void ShowStartDate()
        {
            DoBStartPicker.IsVisible |= Device.RuntimePlatform == Device.Android;
            Device.BeginInvokeOnMainThread(() =>
            {
                DoBStartPicker.Focus();
                DoBStartPicker.IsVisible = false;
            });
        }

        public void ShowEndDate()
        {
            DoBEndPicker.IsVisible |= Device.RuntimePlatform == Device.Android;
            Device.BeginInvokeOnMainThread(() =>
            {
                DoBEndPicker.Focus();
                DoBEndPicker.IsVisible = false;
            });
        }
    }
}
