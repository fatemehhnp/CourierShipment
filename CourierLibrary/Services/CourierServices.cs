using CourierLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierShipment.Services
{
    public class CourierServices: ICourierServices
    {
        public double CalculateShipment(Parcel parcel)
        {
            if (parcel.ParcelType == ParcelType.Small)
            {
                return 3;
            }
            else if (parcel.ParcelType == ParcelType.Medium)
            {
                return 8;
            }
            else if (parcel.ParcelType == ParcelType.Large)
            {
                return 15;
            }
            else
            {
                return 25;
            }
        }
        
    }
}
