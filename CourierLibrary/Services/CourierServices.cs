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
            var discountList = new List<Order>();
            var disCountApplied = false;
            if (smallSizeCount >= Constants.SmallSizeOffer)
            {
                var newDiscount = CalculateDiscount(order, smallSizeOffer, ParcelType.Small);
                discountList.Add(new Order
                {
                    Parcels = order.Parcels,
                    Discount =- newDiscount.Item1,
                    TotalCost = newDiscount.Item2,
                    FastSpeed = order.FastSpeed
                });
                disCountApplied = true;
            }
            if (mediumSizeCount >= Constants.MediumSizeOffer)
            {
                var newDiscount = CalculateDiscount(order, mediumSizeOffer,ParcelType.Medium);
                discountList.Add(new Order
                {
                    Parcels = order.Parcels,
                    Discount = -newDiscount.Item1,
                    TotalCost = newDiscount.Item2,
                    FastSpeed = order.FastSpeed
                });
                disCountApplied = true;
            }
            if (mixedCount >= Constants.MixedOffer)
            {
                var newDiscount = CalculateDiscount(order, mixedOffer);
                discountList.Add(new Order
                {
                    Parcels = order.Parcels,
                    Discount = -newDiscount.Item1,
                    TotalCost = newDiscount.Item2,
                    FastSpeed = order.FastSpeed
                });
                disCountApplied = true;
            }
            if (disCountApplied) order = discountList.Where(x => x.Discount != 0).OrderBy(x => x.TotalCost).FirstOrDefault();
            order.TotalCost = order.FastSpeed ? order.TotalCost * 2 : order.TotalCost;
            return order;

        }
        private (int ,double) CalculateDiscount(Order order, int offerNumber, ParcelType? type = null)
        {

            if (type != null)
            {
                double totalCost = order.TotalCost;
                var offerParcels = order.Parcels.Where(x => x.ParcelType == type);
                var discount = offerParcels.Count() / offerNumber;
                for (var i = 1; i <= discount;i++)
                {
                    var CheapestCost = offerParcels.Min(p => p.Cost);
                    totalCost -= CheapestCost;
                    offerParcels = offerParcels.OrderBy(x => x.Cost).Skip(i);
                }
                return (discount, totalCost);
            }
            else
            {
                var parcels = order.Parcels;
                var totalCost = order.TotalCost;
                var discount = parcels.Count() / offerNumber;
                order.Discount = -discount;
                for (var i = 1; i <= discount; i++)
                {
                    var CheapestCost = parcels.Min(p => p.Cost);
                    totalCost-= CheapestCost;
                    parcels = parcels.OrderBy(x => x.Cost).Skip(i);
                }
                return (discount,totalCost);
            }
        }
    }
}