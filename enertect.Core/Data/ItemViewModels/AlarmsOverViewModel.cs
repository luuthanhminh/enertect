using System;
namespace enertect.Core.Data.ItemViewModels
{
    public class AlarmsOverViewModel: BaseItemViewModel
    {
        private string _totalAlarms;
        public string TotalAlarms
        {
            get
            {
                return _totalAlarms;
            }
            set
            {
                SetProperty(ref _totalAlarms, value);
            }
        }

        private string _openAlarms;
        public string OpenAlarms
        {
            get
            {
                return _openAlarms;
            }
            set
            {
                SetProperty(ref _openAlarms, value);
            }
        }

        private string _criticalAlarms;
        public string CriticalAlarms
        {
            get
            {
                return _criticalAlarms;
            }
            set
            {
                SetProperty(ref _criticalAlarms, value);
            }
        }
    }
}
