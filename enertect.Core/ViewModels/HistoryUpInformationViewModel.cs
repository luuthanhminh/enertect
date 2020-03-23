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

namespace enertect.Core.ViewModels
{
    public interface ICallsViewHistory
    {
        void BindingChart(IList<UpsInformation> datas);
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
            
            GetHistory(this.UpID, DateTime.Now.AddMonths(-3), DateTime.Now);
            GetUpLimit(this.UpID);
        }

        public override void Prepare(UpsItemViewModel parameter)
        {
            this.UpID = parameter.UpsId;

        }

        #region Properties

        public ICallsViewHistory View { get; set; }

        public int UpID { get; set; }

        private bool _hasNoData;
        public bool HasNoData
        {
            get
            {
                return _hasNoData;
            }
            set
            {
                SetProperty(ref _hasNoData, value);
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

        async Task GetUpLimit(int UpId)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var res = await _apiService.getUpLimit(UpId);

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
                    HasNoData = false;
                    ShowLoading();
                    var res = await _apiService.getHistoryUpsInfornations(UpId, start, end);

                    if (res.IsSuccess)
                    {
                        if (res.ResponseListObject.Count > 0)
                        {
                            HasNoData = true;
                            UpInfoHistory = new ObservableCollection<UpsInformation>(res.ResponseListObject[0].UpsHistoryTrendings.Select(v=>v.ConvertDate()));
                            foreach (UpsInformation up in UpInfoHistory)
                            {
                                up.UpsHistoryTrendings.Select(v => v.ConvertDate());
                            }
                            if (View != null)
                            {
                                View.BindingChart(UpInfoHistory);
                            }

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
                //HideLoading();
            }

        }

        #endregion
    }
}
