using System;
using System.Collections.ObjectModel;

namespace enertect.Core.Data.ItemViewModels
{
    public class UpsReadingItemViewModel : BaseItemViewModel
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

        private string _upsId;
        public string UpsId
        {
            get
            {
                return _upsId;
            }
            set
            {
                SetProperty(ref _upsId, value);
            }
        }

        private ObservableCollection<ReadingItemViewModel> _upsReadings = new ObservableCollection<ReadingItemViewModel>();
        public ObservableCollection<ReadingItemViewModel> UpsReadings
        {
            get
            {
                return _upsReadings;
            }
            set
            {
                SetProperty(ref _upsReadings, value);
            }
        }
    }
}
