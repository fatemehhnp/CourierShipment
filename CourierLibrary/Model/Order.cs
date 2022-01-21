using System;
using System.Collections.Generic;
using System.Text;

namespace CourierLibrary.Model
{
    public class Order
    {
        public IEnumerable<Parcel> Parcels { get; set; }
        public decimal Discount { get; set; }
        public double TotalCost { get; set; }
        public bool FastSpeed { get; set; }
    }
}
