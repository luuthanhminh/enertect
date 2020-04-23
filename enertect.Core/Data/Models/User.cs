using System;
using System.Collections.Generic;

namespace enertect.Core.Data.Models
{
    public class User
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string ApiEndpoint { get; set; }
        public int NumberOfSites { get; set; }
        public IList<Site> SitesEndPoints { get; set; }
    }
}
