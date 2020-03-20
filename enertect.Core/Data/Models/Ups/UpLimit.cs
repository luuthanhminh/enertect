using System;
namespace enertect.Core.Data.Models.Ups
{
    public class UpLimit
    {
        public int UpsId { get; set; }
        public string UpsName { get; set; }
        public double VolUp { get; set; }
        public double VolDown { get; set; }
        public double IrUp { get; set; }
        public double IrDown { get; set; }
    }
}
