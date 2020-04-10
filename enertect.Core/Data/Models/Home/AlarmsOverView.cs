using System;
namespace enertect.Core.Data.Models.Home
{
    public class AlarmsOverView
    {
        public string TotalAlarms { get; set; }
        public int OpenAlarms { get; set; }
        public int CriticalAlarms { get; set; }
    }
}
