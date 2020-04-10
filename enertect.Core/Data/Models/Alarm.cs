using System;
namespace enertect.Core.Data.Models
{
    public class Alarm
    {
        public int AlarmId { get; set; }
        public string AlarmDate { get; set; }
        public string AlertMessegeEmail { get; set; }
        public bool ShowError { get; set; }
        public string BatteryName { get; set; }
        public int Upsid { get; set; }
        public string UpsName { get; set; }
        //public bool SentEmail { get; set; }
        public string AlertType { get; set; }
        public bool ProblemResolved { get; set; }
        public string AlertMessegeSms { get; set; }
        public string ProblemResolvedDate { get; set; }
        //public int BatteryConfigId { get; set; }
        //public int ErrorType { get; set; }
        public double AlertValue { get; set; }
        public double? TrueValue { get; set; }
        public string ActionTaken { get; set; }
    }
}
