using System;
using System.Collections.ObjectModel;
using enertect.Core.Data.Models.Ups;

namespace enertect.Core.Data.ItemViewModels
{
    public class UpsItemViewModel : BaseItemViewModel
    {
        public bool Initilaze { get; set; }
        public int UpsId { get; set; }

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

        private string _batteryId;
        public string BatteryId
        {
            get
            {
                return _batteryId;
            }
            set
            {
                SetProperty(ref _batteryId, value);
            }
        }

        private string _stringName;
        public string StringName
        {
            get
            {
                return _stringName;
            }
            set
            {
                SetProperty(ref _stringName, value);
            }
        }

        private string _dateValue;
        public string DateValue
        {
            get
            {
                return _dateValue;
            }
            set
            {
                SetProperty(ref _dateValue, value);
            }
        }

        private string _dateOfTime;
        public string DateOfTime
        {
            get
            {
                return _dateOfTime;
            }
            set
            {
                SetProperty(ref _dateOfTime, value);
            }
        }

        private double _voltage;
        public double Voltage
        {
            get
            {
                return _voltage;
            }
            set
            {
                SetProperty(ref _voltage, value);
            }
        }

        private double _resitance;
        public double Resitance
        {
            get
            {
                return _resitance;
            }
            set
            {
                SetProperty(ref _resitance, value);
            }
        }

        private double _temperature;
        public double Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                SetProperty(ref _temperature, value);
            }
        }


        private ObservableCollection<UpsItemViewModel> _upInfoHistory = new ObservableCollection<UpsItemViewModel>();
        public ObservableCollection<UpsItemViewModel> UpsHistoryTrendings
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

        private ObservableCollection<UpsItemViewModel> _items = new ObservableCollection<UpsItemViewModel>();
        public ObservableCollection<UpsItemViewModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                SetProperty(ref _items, value);
            }
        }


        //private UpsInformation _upsInformation;
        //public UpsInformation UpsInformation
        //{
        //    get
        //    {
        //        return _upsInformation;
        //    }
        //    set
        //    {
        //        SetProperty(ref _upsInformation, value);
        //    }
        //}


    }
}
