using System;
using System.Threading.Tasks;
using enertect.Core.Data.ItemViewModels;
using enertect.Core.Services.Interfaces;
using enertect.Core.ViewModels.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Linq;
using enertect.Core.Data.Models.Ups;

namespace enertect.Core.ViewModels
{
    public class UpInformationDetailViewModel : BaseWithObjectViewModel<UpsItemViewModel>
    {
        readonly IApiService _apiService;
        UpsItemViewModel _upsItemViewModel;

        public UpInformationDetailViewModel(IMvxNavigationService navigationService, IDialogService dialogService, IApiService apiService) : base(navigationService, dialogService)
        {
            this._apiService = apiService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

        }

        public override void Prepare(UpsItemViewModel parameter)
        {
            _upsItemViewModel = parameter;
            _upsName = parameter.UpsInformation.UpsName;
            var item = parameter.UpsInformation;
            if (item.Items.Count > 0)
            {
                var up = item.Items.First();
                var totalvoltage = up.Items.Select(c => c.Voltage).Sum();
                _sumVol = totalvoltage.ToString();

                _minVolItem = up.Items.OrderBy(i => i.Voltage).FirstOrDefault();
                _maxVolItem = up.Items.OrderBy(i => i.Voltage).LastOrDefault();
                _avgVol = ((Convert.ToDecimal(_maxVolItem.Voltage) - Convert.ToDecimal(totalvoltage/ up.Items.Count))/1000).ToString();
                _maxVol = ((Convert.ToDecimal(_maxVolItem.Voltage) - Convert.ToDecimal(_minVolItem.Voltage))/1000).ToString();

                _minIRItem = up.Items.OrderBy(i => i.Resitance).FirstOrDefault();
                _maxIRItem = up.Items.OrderBy(i => i.Resitance).LastOrDefault();
                _avgIR = (up.Items.Select(c => c.Resitance).Sum()/up.Items.Count).ToString();

                _minTempItem = up.Items.OrderBy(i => i.Temperature).FirstOrDefault();
                _maxTempItem = up.Items.OrderBy(i => i.Temperature).LastOrDefault();
                _avgTemp = (up.Items.Select(c => c.Temperature).Sum() / up.Items.Count).ToString();
            }
        }

        #region Properties

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

        private UpsInformation _minVolItem;
        public UpsInformation MinVolItem
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

        private UpsInformation _maxVolItem;
        public UpsInformation MaxVolItem
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

        private UpsInformation _minIRItem;
        public UpsInformation MinIRItem
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

        private UpsInformation _maxIRItem;
        public UpsInformation MaxIRItem
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

        private UpsInformation _minTempItem;
        public UpsInformation MinTempItem
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

        private UpsInformation _maxTempItem;
        public UpsInformation MaxTempItem
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

        #region Commands



        #endregion
    }
}
