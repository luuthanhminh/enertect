using System;
using System.Collections.ObjectModel;

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

        private int _openAlarms;
        public int OpenAlarms
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

        private int _criticalAlarms;
        public int CriticalAlarms
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

        private ObservableCollection<AlarmOverViewItemModel> _overviews = new ObservableCollection<AlarmOverViewItemModel>();
        public ObservableCollection<AlarmOverViewItemModel> Overviews
        {
            get
            {
                return _overviews;
            }
            set
            {
                SetProperty(ref _overviews, value);
            }
        }
    }
}
