using System;
using System.Collections.ObjectModel;

namespace enertect.Core.Data.ItemViewModels
{
    public class UpsViewModel : BaseItemViewModel
    {
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
    }
}
