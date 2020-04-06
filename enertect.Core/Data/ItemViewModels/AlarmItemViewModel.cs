using System;
namespace enertect.Core.Data.ItemViewModels
{
    public class AlarmItemViewModel : BaseItemViewModel
    {
        public bool Initilaze { get; set; }

        private string _color;
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                SetProperty(ref _color, value);
            }
        }

        private string _alarmTime;
        public string AlarmTime
        {
            get
            {
                return _alarmTime;
            }
            set
            {
                SetProperty(ref _alarmTime, value);
            }
        }

        private string _alarmDate;
        public string AlarmDate
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

        private double _alertValue;
        public double AlertValue
        {
            get
            {
                return _alertValue;
            }
            set
            {
                SetProperty(ref _alertValue, value);
            }
        }

        private double? _resolveValue;
        public double? ResolveValue
        {
            get
            {
                return _resolveValue;
            }
            set
            {
                SetProperty(ref _resolveValue, value);
            }
        }

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        private string _actionTaken;
        public string ActionTaken
        {
            get
            {
                return _actionTaken;
            }
            set
            {
                SetProperty(ref _actionTaken, value);
            }
        }

        private string _problemResolvedDate;
        public string ProblemResolvedDate
        {
            get
            {
                return _problemResolvedDate;
            }
            set
            {
                SetProperty(ref _problemResolvedDate, value);
            }
        }

        private double _resolvedDate;
        public double ResolvedDate
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

        private string _brand;
        public string Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                SetProperty(ref _brand, value);
            }
        }
    }
}
