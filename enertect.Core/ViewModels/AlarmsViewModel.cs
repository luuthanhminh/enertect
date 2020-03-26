using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Helpers;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Essentials;

namespace enertect.Core.ViewModels
{
    public interface ICallsAlarmsView
    {
        void ShowResolvedDate();
        void ShowAlarmDate();
    }

    public class AlarmsViewModel: BaseWithObjectViewModel<UpsViewModel>
    {
        readonly IApiService _apiService;


        public AlarmsViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            this._apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            AlarmDate = DateTime.Now;
            ResolvedDate = DateTime.Now;
            AlertType = "";
            SearchValue = "";
            UpsName = "";
            AlertValue = "";
            ResolveValue = "";
            LoadData();
        }

        public override void Prepare(UpsViewModel parameter)
        {
            Ups = parameter.Ups.ToList();
        }

        #region Properties

        public ICallsAlarmsView View { get; set; }
        public IList<UpsItemViewModel> Ups { get; set; }
        public IList<AlarmItemViewModel> OriginalDatas { get; set; }

        private string _searchValue;
        public string SearchValue
        {
            get
            {
                return _searchValue;
            }
            set
            {
                if(_searchValue != value)
                {
                    FilterData(value.Trim().ToLower());
                }
                SetProperty(ref _searchValue, value);
            }
        }

        private string _alertType;
        public string AlertType
        {
            get
            {
                return _alertType;
            }
            set
            {
                
                SetProperty(ref _alertType, value);
                FilterData();
            }
        }

        private string _upsName;
        public string UpsName
        {
            get
            {
                return _upsName;
            }
            set
            {
                SetProperty(ref _upsName, value);
                FilterData();
            }
        }

        private string _alertValue;
        public string AlertValue
        {
            get
            {
                return _alertValue;
            }
            set
            {
                SetProperty(ref _alertValue, value);
                FilterData();
            }
        }

        private string _resolveValue;
        public string ResolveValue
        {
            get
            {
                return _resolveValue;
            }
            set
            {
                SetProperty(ref _resolveValue, value);
                FilterData();
            }
        }

        private DateTime _alarmDate;
        public DateTime AlarmDate
        {
            get
            {
                return _alarmDate;
            }
            set
            {
                SetProperty(ref _alarmDate, value);
            }
        }

        private DateTime _resolvedDate;
        public DateTime ResolvedDate
        {
            get
            {
                return _resolvedDate;
            }
            set
            {
                SetProperty(ref _resolvedDate, value);
            }
        }

        private ObservableCollection<AlarmItemViewModel> _alarmDatas = new ObservableCollection<AlarmItemViewModel>();
        public ObservableCollection<AlarmItemViewModel> AlarmDatas
        {
            get
            {
                return _alarmDatas;
            }
            set
            {
                SetProperty(ref _alarmDatas, value);
            }
        }

        private ObservableCollection<AlarmItemViewModel> _alarms = new ObservableCollection<AlarmItemViewModel>();
        public ObservableCollection<AlarmItemViewModel> Alarms
        {
            get
            {
                return _alarms;
            }
            set
            {
                SetProperty(ref _alarms, value);
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
                SetProperty(ref _hasNoData, value);
            }
        }

        #endregion

        #region Methods

        async Task LoadData()
        {
            try
            {

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    HasNoData = false;
                    this.Alarms.Clear();
                    ShowLoading();

                    var res = await _apiService.getAlarms();

                    if (res.IsSuccess)
                    {
                        var alarms = res.ResponseObject.Data.OrderByDescending(e=>e.AlarmDate);
                        OriginalDatas = new ObservableCollection<AlarmItemViewModel>(alarms.Select(v => v.ToAlarmItemViewModel(Ups)));
                        AlarmDatas = new ObservableCollection<AlarmItemViewModel>(OriginalDatas.ToList());
                        Alarms = new ObservableCollection<AlarmItemViewModel>(AlarmDatas.Take(AppConstant.PAGE_SIZE));
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
                HasNoData = this.Alarms.Count == 0;

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

        void FilterData(string key = "")
        {
            if (OriginalDatas != null && OriginalDatas.Any())
            {
                var FilterData = new List<AlarmItemViewModel>();
                if (String.IsNullOrEmpty(key))
                {
                    FilterData = OriginalDatas.Where(e => e.AlertType.ToLower().Contains(AlertType.ToLower())
                   && e.UpsName.ToLower().Contains(UpsName.ToLower())
                   && e.AlertValue.ToString().Contains(AlertValue)
                   && e.ResolveValue.ToString().Contains(ResolveValue)).ToList();
                }
                else
                {
                    FilterData = OriginalDatas.Where(e => e.AlertType.ToLower().Contains(key)
                    || e.UpsName.ToLower().Contains(key)
                    || e.StringName.ToLower().Contains(key)
                    || e.AlertValue.ToString().Contains(key)
                    || e.ResolveValue.ToString().Contains(key)).ToList();
                }

                AlarmDatas = new ObservableCollection<AlarmItemViewModel>(FilterData);
                Alarms = new ObservableCollection<AlarmItemViewModel>(AlarmDatas.Take(AlarmDatas.Count > AppConstant.PAGE_SIZE ? AppConstant.PAGE_SIZE : AlarmDatas.Count));
            } 
        }



        #endregion

        #region Commands

        public IMvxCommand LoadMoreItemsCommand => new MvxCommand(LoadMoreItems);

        private void LoadMoreItems()
        {
            if (AlarmDatas.Any())
            {
                var start = Alarms.Count;
                var lenght = start + AppConstant.PAGE_SIZE < AlarmDatas.Count ? start + AppConstant.PAGE_SIZE : AlarmDatas.Count;
                for (int i = start; i < lenght; i++)
                {
                    Alarms.Add(AlarmDatas[i]);
                }

            }
            
        }

        public IMvxCommand TapDoBAlarmCommand => new MvxCommand(showDoBAlarmDate);

        void showDoBAlarmDate()
        {
            if (View != null)
            {
                View.ShowAlarmDate();
            }
        }

        public IMvxCommand TapDoBResolvedCommand => new MvxCommand(showDoBResolvedDate);

        void showDoBResolvedDate()
        {
            if (View != null)
            {
                View.ShowResolvedDate();
            }
        }

        #endregion
    }
}
