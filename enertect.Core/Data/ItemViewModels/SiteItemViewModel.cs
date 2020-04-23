using System;
namespace enertect.Core.Data.ItemViewModels
{
    public class SiteItemViewModel: BaseItemViewModel
    {
        private string _siteUrl;
        public string SiteUrl
        {
            get
            {
                return _siteUrl;
            }
            set
            {
                SetProperty(ref _siteUrl, value);
            }
        }

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
    }
}
