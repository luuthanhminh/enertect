using System;
namespace enertect.Core.Data.ItemViewModels
{
    public class OverviewItemViewModel: BaseItemViewModel
    {
        private string _siteName;
        public string SiteName
        {
            get
            {
                return _siteName;
            }
            set
            {
                SetProperty(ref _siteName, value);
            }
        }

        private string _siteLocation;
        public string SiteLocation
        {
            get
            {
                return _siteLocation;
            }
            set
            {
                SetProperty(ref _siteLocation, value);
            }
        }

        private string _enertectModel;
        public string EnertectModel
        {
            get
            {
                return _enertectModel;
            }
            set
            {
                SetProperty(ref _enertectModel, value);
            }
        }

        private string _siteEmail;
        public string SiteEmail
        {
            get
            {
                return _siteEmail;
            }
            set
            {
                SetProperty(ref _siteEmail, value);
            }
        }

        private string _siteContact;
        public string SiteContact
        {
            get
            {
                return _siteContact;
            }
            set
            {
                SetProperty(ref _siteContact, value);
            }
        }

        private string _totalUPS;
        public string TotalUPS
        {
            get
            {
                return _totalUPS;
            }
            set
            {
                SetProperty(ref _totalUPS, value);
            }
        }

        private string _totalStrings;
        public string TotalStrings
        {
            get
            {
                return _totalStrings;
            }
            set
            {
                SetProperty(ref _totalStrings, value);
            }
        }

        private string _totalBatteries;
        public string TotalBatteries
        {
            get
            {
                return _totalBatteries;
            }
            set
            {
                SetProperty(ref _totalBatteries, value);
            }
        }
    }
}
