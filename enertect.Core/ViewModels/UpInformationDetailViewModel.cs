﻿using System;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Linq;
using enertect.Core.Data.Models.Ups;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Essentials;
using enertect.Core.Helpers;

namespace enertect.Core.ViewModels
{
    public interface ICallsView
    {
        void BindingChart();
    }

    public class UpInformationDetailViewModel : BaseWithObjectViewModel<UpsItemViewModel>
    {
        readonly IApiService _apiService;

        public UpInformationDetailViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            _apiService = apiService; 
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            GetUpLimit(UpID);
        }

        public override void Prepare(UpsItemViewModel parameter)
        {
            _dateNow = DateTime.Now;
            _itemViewModel = parameter;
            _upsName = parameter.StringName.ToUpper();
            UpID = parameter.UpsId;
            if (parameter.Items.Count > 0)
            {
                var up = parameter;
                //if(up.Items == null || !up.Items.Any())
                //{
                //    up = parameter;
                //}
                var totalitems = up.Items.Count;
                var totalvoltage = up.Items.Select(c => c.Voltage).Sum();
                var averageVoltage = totalvoltage / totalitems;
                _sumVol = totalvoltage.ToString();

                _minVolItem = up.Items.OrderBy(i => i.Voltage).FirstOrDefault();
                _maxVolItem = up.Items.OrderBy(i => i.Voltage).LastOrDefault();
                _avgVol = Convert.ToDecimal(((Convert.ToDecimal(_maxVolItem.Voltage) - Convert.ToDecimal(averageVoltage / totalitems))/1000)).ToString("0.###");
                _maxVol = ((Convert.ToDecimal(_maxVolItem.Voltage) - Convert.ToDecimal(_minVolItem.Voltage))/1000).ToString("0.###");

                _minIRItem = up.Items.OrderBy(i => i.Resitance).FirstOrDefault();
                _maxIRItem = up.Items.OrderBy(i => i.Resitance).LastOrDefault();
                var totalRI = up.Items.Select(c => c.Resitance).Sum();
                var averageRI = totalRI / totalitems;
                _avgIR = Convert.ToDecimal(((Convert.ToDecimal(_maxIRItem.Resitance) - Convert.ToDecimal(averageRI / totalitems)) / 1000)).ToString("0.###");
                _maxIR = ((Convert.ToDecimal(_maxIRItem.Resitance) - Convert.ToDecimal(_minIRItem.Resitance)) / 1000).ToString("0.###");

                _minTempItem = up.Items.OrderBy(i => i.Temperature).FirstOrDefault();
                _maxTempItem = up.Items.OrderBy(i => i.Temperature).LastOrDefault();
                var totalTemp = up.Items.Select(c => c.Temperature).Sum();
                var averageTemp = totalTemp / totalitems;
                _avgTemp = Convert.ToDecimal(((Convert.ToDecimal(_maxTempItem.Temperature) - Convert.ToDecimal(averageTemp / totalitems)) / 1000)).ToString("0.###");
                _maxTemp = ((Convert.ToDecimal(_maxTempItem.Temperature) - Convert.ToDecimal(_minTempItem.Temperature)) / 1000).ToString("0.###");

                foreach (UpsItemViewModel upInformation in up.Items)
                {
                    _ups.Add(upInformation);
                }
            }
        }

        #region Properties

        public ICallsView View { get; set; }

        public int UpID { get; set; }

        private UpsItemViewModel _itemViewModel;
        public UpsItemViewModel ItemViewModel
        {
            get
            {
                return _itemViewModel;
            }
            set
            {
                SetProperty(ref _itemViewModel, value);
            }
        }

        private UpLimitViewModel _upLimit;
        public UpLimitViewModel UpLimit
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

        private ObservableCollection<UpsItemViewModel> _ups = new ObservableCollection<UpsItemViewModel>();
        public ObservableCollection<UpsItemViewModel> Ups
        {
            get
            {
                return _ups;
            }
            set
            {
                SetProperty(ref _ups, value);
            }
        }

        private DateTimeOffset _dateNow;
        public DateTimeOffset DateNow
        {
            get
            {
                return _dateNow;
            }
            set
            {
                SetProperty(ref _dateNow, value);
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
            }
        }

        private string _sumVol;
        public string SumVol
        {
            get
            {
                return _sumVol;
            }
            set
            {
                SetProperty(ref _sumVol, value);
            }
        }

        private string _avgVol;
        public string AvgVol
        {
            get
            {
                return _avgVol;
            }
            set
            {
                SetProperty(ref _avgVol, value);
            }
        }

        private string _maxVol;
        public string MaxVol
        {
            get
            {
                return _maxVol;
            }
            set
            {
                SetProperty(ref _maxVol, value);
            }
        }

        private string _avgIR;
        public string AvgIR
        {
            get
            {
                return _avgIR;
            }
            set
            {
                SetProperty(ref _avgIR, value);
            }
        }
        private string _maxIR;
        public string MaxIR
        {
            get
            {
                return _maxIR;
            }
            set
            {
                SetProperty(ref _maxIR, value);
            }
        }
        private string _maxTemp;
        public string MaxTemp
        {
            get
            {
                return _maxTemp;
            }
            set
            {
                SetProperty(ref _maxTemp, value);
            }
        }

        private string _avgTemp;
        public string AvgTemp
        {
            get
            {
                return _avgTemp;
            }
            set
            {
                SetProperty(ref _avgTemp, value);
            }
        }

        private UpsItemViewModel _minVolItem;
        public UpsItemViewModel MinVolItem
        {
            get
            {
                return _minVolItem;
            }
            set
            {
                SetProperty(ref _minVolItem, value);
            }
        }

        private UpsItemViewModel _maxVolItem;
        public UpsItemViewModel MaxVolItem
        {
            get
            {
                return _maxVolItem;
            }
            set
            {
                SetProperty(ref _maxVolItem, value);
            }
        }

        private UpsItemViewModel _minIRItem;
        public UpsItemViewModel MinIRItem
        {
            get
            {
                return _minIRItem;
            }
            set
            {
                SetProperty(ref _minIRItem, value);
            }
        }

        private UpsItemViewModel _maxIRItem;
        public UpsItemViewModel MaxIRItem
        {
            get
            {
                return _maxIRItem;
            }
            set
            {
                SetProperty(ref _maxIRItem, value);
            }
        }

        private UpsItemViewModel _minTempItem;
        public UpsItemViewModel MinTempItem
        {
            get
            {
                return _minTempItem;
            }
            set
            {
                SetProperty(ref _minTempItem, value);
            }
        }

        private UpsItemViewModel _maxTempItem;
        public UpsItemViewModel MaxTempItem
        {
            get
            {
                return _maxTempItem;
            }
            set
            {
                SetProperty(ref _maxTempItem, value);
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
                        UpLimit = res.ResponseObject.ToUpLimitViewModel();
                        if (View != null)
                        {
                            View.BindingChart();
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

        }

        #endregion

        #region Commands

        public IMvxAsyncCommand TapHistoryCommand => new MvxAsyncCommand(GoToHistory);

        async Task GoToHistory()
        {
            await _navigationService.Navigate<HistoryUpInformationViewModel, UpsItemViewModel>(ItemViewModel);
        }


        #endregion
    }
}
