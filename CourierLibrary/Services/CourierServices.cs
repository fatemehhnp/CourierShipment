using CourierLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierShipment.Services
{
    public class CourierServices: ICourierServices
    {
        public double CalculateBasicShipmentCost(Parcel parcel)
        {
            var weight = parcel.Weight;
            var parcelType = parcel.ParcelType;
            if (parcelType == ParcelType.Small && weight <= 1)
                return 3;
            else if (parcelType == ParcelType.Small && weight > 1)
                return (3 + ((weight - 1) * 2));
            else if (parcelType == ParcelType.Medium && weight <= 3)
                return 8;
            else if (parcelType == ParcelType.Medium && weight > 3)
                return (8 + ((weight - 3) * 2));
            else if (parcelType == ParcelType.Large && weight <= 6)
                return 15;
            else if (parcelType == ParcelType.Large && weight > 6)
                return (15 + ((weight - 6) * 2));
            else if (parcelType == ParcelType.Xlarge && weight <= 10)
                return 25;
            else if (parcelType == ParcelType.Xlarge && weight > 10)
                return (25 + ((weight - 10) * 2));
            else if (parcelType == ParcelType.Heavy && weight <= 50)
                return 50;
            else 
                return (50 + (weight - 50));
        }
        public Order CalculateShipmentCostToConsiderFastShippment(Order order)
        {
            if (order.FastSpeed)
                order.TotalCost = order.TotalCost * 2;
            return order;
        }

    }
}
