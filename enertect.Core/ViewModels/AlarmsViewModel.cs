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
            LoadData();
        }

        public override void Prepare(UpsViewModel parameter)
        {
            Ups = parameter.Ups.ToList();
        }

        #region Properties

        public IList<UpsItemViewModel> Ups { get; set; }

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
                        AlarmDatas = new ObservableCollection<AlarmItemViewModel>(alarms.Select(v => v.ToAlarmItemViewModel(Ups)));
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
        #endregion

        #region Commands

        public IMvxCommand LoadMoreItemsCommand => new MvxCommand(LoadMoreItems);

        private void LoadMoreItems()
        {
            if (AlarmDatas.Any())
            {
                var start = Alarms.Count - 1;
                var lenght = start + AppConstant.PAGE_SIZE < AlarmDatas.Count ? start + AppConstant.PAGE_SIZE : AlarmDatas.Count;
                for (int i = start; i < lenght; i++)
                {
                    Alarms.Add(AlarmDatas[i]);
                }

            }
            
        }
        #endregion
    }
}
