using CourierLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierShipment.Services
{
    public interface ICourierServices
    {
        double CalculateBasicShipmentCost(Parcel parcel);
        Order CalculateShipmentCostToConsiderFastShippment(Order parcel);
        Order CalculateOrderCost(Order order);
    }
}
