using System;
namespace enertect.Core.Data.ItemViewModels
{
    public class AlarmOverViewItemModel:BaseItemViewModel
    {
        private string _overViewName;
        public string OverViewName
        {
            get
            {
                return _overViewName;
            }
            set
            {
                SetProperty(ref _overViewName, value);
            }
        }

        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref _value, value);
            }
        }
    }
}
