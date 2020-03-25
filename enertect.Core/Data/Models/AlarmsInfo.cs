using System;
using System.Collections.Generic;

namespace enertect.Core.Data.Models
{
    public class AlarmsInfo
    {
        public int TotalCount { get; set; }
        public int GroupCount { get; set; }
        public IList<Alarm> Data { get; set; }
    }
}
