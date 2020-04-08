using System;
using System.Collections.Generic;

namespace enertect.Core.Helpers
{
    public class Utils
    {
        public static string RelativeDate(DateTime ResolvedDate, DateTime AlarmDate)
        {
            if (ResolvedDate == null || AlarmDate == null) return "";
            TimeSpan t = new TimeSpan(ResolvedDate.Ticks - AlarmDate.Ticks);
            var timeSpane = $"{t.Days.ToString()} days :{t.Hours.ToString()} hours :{t.Minutes.ToString()} minutes";
            return timeSpane;

        }
    }
}
