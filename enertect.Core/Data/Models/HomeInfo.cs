using System;
using System.Collections.Generic;
using enertect.Core.Data.Models.Home;

namespace enertect.Core.Data.Models
{
    public class HomeInfo
    {
        public OverView UpsOverview { get; set; }
        public IList<Alarm> CriticalAlarmsList { get; set; }
        public AlarmsOverView AlarmsOverView { get; set; }
        public IList<UpsReadingsList> upsReadingsList { get; set; }
    }
}
