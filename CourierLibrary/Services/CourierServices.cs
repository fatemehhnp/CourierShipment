using CourierLibrary;
using CourierLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourierShipment.Services
{
    public class CourierServices : ICourierServices
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

        public Order CalculateOrderCost(Order order)
        {
            foreach (var parcel in order.Parcels)
            {
                parcel.Cost = CalculateBasicShipmentCost(parcel);
                order.TotalCost += CalculateBasicShipmentCost(parcel);
            }
            var smallSizeOffer = Constants.SmallSizeOffer;
            var mediumSizeOffer = Constants.MediumSizeOffer;
            var mixedOffer = Constants.MixedOffer;
            var smallSizeCount = order.Parcels.Count(x => x.ParcelType == ParcelType.Small);
            var mediumSizeCount = order.Parcels.Count(x => x.ParcelType == ParcelType.Medium);
            var mixedCount = order.Parcels.Count();
            if (smallSizeCount >= Constants.SmallSizeOffer)
            {
                order = CalculateDiscount(order, smallSizeOffer, ParcelType.Small);
            }
            if (mediumSizeCount >= Constants.MediumSizeOffer)
            {
                order = CalculateDiscount(order, mediumSizeOffer, ParcelType.Medium);
            }
            if (mixedCount >= Constants.MixedOffer)
            {
                order = CalculateDiscount(order, mixedOffer);
            }
            order.TotalCost = order.FastSpeed ? order.TotalCost * 2 : order.TotalCost;
            return order;

        }
        private Order CalculateDiscount(Order order, int offerNumber, ParcelType? type = null)
        {
            if (type != null)
            {
                var offerParcels = order.Parcels.Where(x => x.ParcelType == type);
                var discountGroup = offerParcels.Count() / offerNumber;
                if (discountGroup == 1)
                {
                    var CheapestCost = offerParcels.Min(p => p.Cost);
                    order.Discount = -1;
                    order.TotalCost -= CheapestCost;
                    return order;
                }
                else
                {
                    return new Order();
                }
            }
            else
            {
                var discountGroup = order.Parcels.Count() / offerNumber;
                if (discountGroup == 1)
                {
                    var CheapestCost = order.Parcels.Min(p => p.Cost);
                    order.Discount = -1;
                    order.TotalCost -= CheapestCost;
                    return order;
                }
                else
                {
                    return new Order();
                }
            }
        }
    }
}