using System;
using System.Collections.Generic;
using System.Text;

namespace CourierLibrary.Model
{
    public class Parcel
    {
        public decimal Dimension { get; set; }
        public ParcelType ParcelType { get; set; }
        public double Weight { get; set; }
    }
}
