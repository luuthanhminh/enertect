using System;
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

        private UpsInformation _upsInformation;
        public UpsInformation UpsInformation
        {
            get
            {
                return _upsInformation;
            }
            set
            {
                SetProperty(ref _upsInformation, value);
            }
        }

        
    }
}
