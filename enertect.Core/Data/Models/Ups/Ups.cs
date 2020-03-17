using System;
namespace enertect.Core.Data.Models.Ups
{
    public class Ups
    {
        public int UpsId { get; set; }
        public string UpsName { get; set; }
        public string StringName { get; set; }
        public string StringName2 { get; set; }
        public int BattConfigId { get; set; }
        public double Voltage { get; set; }
        public double Resitance { get; set; }
        public double Temperature { get; set; }
        public string VoltageStatus { get; set; }
        public string ResistanceStaus { get; set; }
        public string TempStatus { get; set; }
    }
}
