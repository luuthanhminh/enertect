using System;
namespace enertect.Core.Data.ItemViewModels
{
    public class ReadingItemViewModel:BaseItemViewModel
    {
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

        private string _stringVoltage;
        public string StringVoltage
        {
            get
            {
                return _stringVoltage;
            }
            set
            {
                SetProperty(ref _stringVoltage, value);
            }
        }

        private string _stringIR;
        public string StringIR
        {
            get
            {
                return _stringIR;
            }
            set
            {
                SetProperty(ref _stringIR, value);
            }
        }

        private string _stringTemperature;
        public string StringTemperature
        {
            get
            {
                return _stringTemperature;
            }
            set
            {
                SetProperty(ref _stringTemperature, value);
            }
        }

        private string _alarmsReadings;
        public string AlarmsReadings
        {
            get
            {
                return _alarmsReadings;
            }
            set
            {
                SetProperty(ref _alarmsReadings, value);
            }
        }
    }
}
