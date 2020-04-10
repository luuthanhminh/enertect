using System;
using System.Collections.Generic;

namespace enertect.Core.Data.Models.Home
{
    public class UpsReadingsList
    {
        public string UpsName { get; set; }
        public string UpsId { get; set; }
        public IList<Reading> UpsReadings { get; set; }
    }
}
