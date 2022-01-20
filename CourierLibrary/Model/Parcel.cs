using System;
using System.Collections.Generic;
using System.Text;

namespace CourierLibrary.Model
{
    public class Parcel
    {
        public double Dimension { get; set; }
        public ParcelType ParcelType { get; set; }
        public bool FastSpeed { get; set; }
    }
}
