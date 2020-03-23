using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Data.Models.Ups;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Navigation;
using Xamarin.Essentials;
using System.Linq;
using enertect.Core.Helpers;
using System.Collections.Generic;
using MvvmCross.Commands;

namespace enertect.Core.ViewModels
{
    public interface ICallsViewHistory
    {
        void BindingChart(IList<UpsInformation> datas);
        void ShowStartDate();
        void ShowEndDate();
    }

    public class HistoryUpInformationViewModel : BaseWithObjectViewModel<UpsItemViewModel>
    {
        readonly IApiService _apiService;

        public HistoryUpInformationViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            this._apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            LoadData(StartDate, EndDate);
        }

        public override void Prepare(UpsItemViewModel parameter)
        {
            this.UpID = parameter.UpsId;
            _hasNoData = true;
            StartDate = DateTime.Now.AddDays(-7);
            EndDate = DateTime.Now;

        }

        #region Properties

        public ICallsViewHistory View { get; set; }

        public int UpID { get; set; }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                SetProperty(ref _startDate, value);
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                SetProperty(ref _endDate, value);
            }
        }

        private bool _hasNoData;
        public bool HasNoData
        {
            get
            {
                return _hasNoData;
            }
            set
            {
                HasData = !value;
                SetProperty(ref _hasNoData, value);
            }
        }

        private bool _hasData;
        public bool HasData
        {
            get
            {
                return _hasData;
            }
            set
            {
                SetProperty(ref _hasData, value);
            }
        }

        private UpLimit _upLimit;
        public UpLimit UpLimit
        {
            get
            {
                return _upLimit;
            }
            set
            {
                SetProperty(ref _upLimit, value);
            }
        }

        private ObservableCollection<UpsInformation> _upInfoHistory = new ObservableCollection<UpsInformation>();
        public ObservableCollection<UpsInformation> UpInfoHistory
        {
            get
            {
                return _upInfoHistory;
            }
            set
            {
                SetProperty(ref _upInfoHistory, value);
            }
        }

        private string _voltageHistory;
        public string VoltageHistory
        {
            get
            {
                return _voltageHistory;
            }
            set
            {
                SetProperty(ref _voltageHistory, value);
            }
        }

        private string _resistanceHistory;
        public string ResistanceHistory
        {
            get
            {
                return _resistanceHistory;
            }
            set
            {
                SetProperty(ref _resistanceHistory, value);
            }
        }

        private string _temperatureHistory;
        public string TemperatureHistory
        {
            get
            {
                return _temperatureHistory;
            }
            set
            {
                SetProperty(ref _temperatureHistory, value);
            }
        }

        #endregion

        #region Methods

        void LoadData(DateTime start, DateTime end)
        {
           StartDate = start;
           EndDate = end;
           GetHistory(this.UpID, StartDate, EndDate);
           GetUpLimit(this.UpID, StartDate, EndDate);
        }

        async Task GetUpLimit(int UpId, DateTimeOffset start, DateTimeOffset end)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var res = await _apiService.getUpLimitHistory(UpId, start, end);

                    if (res.IsSuccess)
                    {
                        UpLimit = res.ResponseObject.ToUpLimit();
                    }
                    else
                    {
                        await _dialogService.ShowMessage("Error", String.Join(Environment.NewLine, res.Errors), "Close");
                    }
                }
                else
                {
                    await _dialogService.ShowMessage("Error", "No internet access", "Close");
                }

            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessage("Error", ex.Message, "Close");
            }

        }

        async Task GetHistory(int UpId, DateTimeOffset start, DateTimeOffset end)
        {
            _voltageHistory = $"Voltage History from - {start.ToString("dd MMM yyyy")} - {end.ToString("dd MMM yyyy")}";
            _resistanceHistory = $"Resistance History from - {start.ToString("dd MMM yyyy")} - {end.ToString("dd MMM yyyy")}";
            _temperatureHistory = $"Temperature History from - {start.ToString("dd MMM yyyy")} - {end.ToString("dd MMM yyyy")}";
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HasNoData = true;
                    ShowLoading();
                    var res = await _apiService.getHistoryUpsInfornations(UpId, start, end);

                    if (res.IsSuccess)
                    {
                        if (res.ResponseListObject.Count > 0)
                        {
                            UpInfoHistory = new ObservableCollection<UpsInformation>(res.ResponseListObject[0].UpsHistoryTrendings.Select(v=>v.ConvertDate()));
                            foreach (UpsInformation up in UpInfoHistory)
                            {
                                up.UpsHistoryTrendings.Select(v => v.ConvertDate());
                            }
                            if (View != null)
                            {
                                View.BindingChart(UpInfoHistory);
                            }
                            HasNoData = false;
                        }
                    }
                    else
                    {
                        await _dialogService.ShowMessage("Error", String.Join(Environment.NewLine, res.Errors), "Close");
                    }
                }
                else
                {
                    await _dialogService.ShowMessage("Error", "No internet access", "Close");
                }

            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessage("Error", ex.Message, "Close");
            }
            finally
            {
                HideLoading();
            }

        }

        #endregion

        #region Commands

        public IMvxCommand TapOneWeekCommand => new MvxCommand(GetWeekHistory);

        void GetWeekHistory()
        {
            LoadData(DateTime.Now.AddDays(-7), DateTime.Now);
        }

        public IMvxCommand TapOneMonthCommand => new MvxCommand(GetMonthHistory);

        void GetMonthHistory()
        {
            LoadData(DateTime.Now.AddMonths(-1), DateTime.Now);
        }

        public IMvxCommand TapThreeMonthCommand => new MvxCommand(GetThreeHistory);

        void GetThreeHistory()
        {
            LoadData(DateTime.Now.AddMonths(-3), DateTime.Now);
        }

        public IMvxCommand TapLoadHistoryCommand => new MvxCommand(GetLoadHistory);

        void GetLoadHistory()
        {
            LoadData(StartDate, EndDate);
        }

        public IMvxCommand TapDobStartCommand => new MvxCommand(showDoBStartDate);

        void showDoBStartDate()
        {
            if(View != null)
            {
                View.ShowStartDate();
            }
        }

        public IMvxCommand TapDobEndCommand => new MvxCommand(showDoBEndDate);

        void showDoBEndDate()
        {
            if (View != null)
            {
                View.ShowEndDate();
            }
        }

        #endregion
    }
}
