using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using enertect.Core.ViewModels;
using enertect.UI.Pages.Base;
using enertect.UI.Services.Interfaces;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
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
                    this.Chart.Legend = new ChartLegend()
                    {
                        OverflowMode = ChartLegendOverflowMode.Wrap,
                        DockPosition = LegendPlacement.Top,
                        IconHeight = 10,
                        IconWidth = 10,
                        ToggleSeriesVisibility = true,
                        AutomationId = i.ToString()
                    };
                    this.Chart.Series.Add(columnSeries);
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
    }
}
