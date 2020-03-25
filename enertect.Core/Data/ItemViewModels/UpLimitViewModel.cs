using System;
namespace enertect.Core.Data.ItemViewModels
{
    public class UpLimitViewModel:BaseItemViewModel
    {
        public bool Initilaze { get; set; }

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

        private double _volUp;
        public double VolUp
        {
            get
            {
                return _volUp;
            }
            set
            {
                SetProperty(ref _volUp, value);
            }
        }

        private double _volDown;
        public double VolDown
        {
            get
            {
                return _volDown;
            }
            set
            {
                SetProperty(ref _volDown, value);
            }
        }

        private double _irUp;
        public double IrUp
        {
            get
            {
                return _irUp;
            }
            set
            {
                SetProperty(ref _irUp, value);
            }
        }

        private double _irDown;
        public double IrDown
        {
            get
            {
                return _irDown;
            }
            set
            {
                SetProperty(ref _irDown, value);
            }
        }

        private double _tempUp;
        public double TempUp
        {
            get
            {
                return _tempUp;
            }
            set
            {
                SetProperty(ref _tempUp, value);
            }
        }

        private double _tempDown;
        public double TempDown
        {
            get
            {
                return _tempDown;
            }
            set
            {
                SetProperty(ref _tempDown, value);
            }
        }

    }
}
