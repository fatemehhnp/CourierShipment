using CourierLibrary.Model;
using CourierShipment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CourierShipment.Test
{
    public class CourierServicesTests
    {
        private readonly ICourierServices _sut;
        public CourierServicesTests()
        {
            _sut = new CourierServices();
        }
        [Fact]
        public void CalculateBasicShipmentCost_WithSmallType_ShouldReturn3()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 5,
                ParcelType = ParcelType.Small
            };
            //Act
            var result = _sut.CalculateBasicShipmentCost(parcel);
            //Assert
            Assert.Equal(3, result);
        }
        [Fact]
        public void CalculateBasicShipmentCost_WithMediumType_ShouldReturn8()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 40,
                ParcelType = ParcelType.Medium
            };
            //Act
            var result = _sut.CalculateBasicShipmentCost(parcel);
            //Assert
            Assert.Equal(8, result);
        }
        [Fact]
        public void CalculateBasicShipmentCost_WithLarge_ShouldReturn15()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 80,
                ParcelType = ParcelType.Large
            };
            //Act
            var result = _sut.CalculateBasicShipmentCost(parcel);
            //Assert
            Assert.Equal(15, result);
        }
        [Fact]
        public void CalculateBasicShipmentCost_WithXLargeType_ShouldReturn25()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 300,
                ParcelType = ParcelType.Xlarge
            };
            //Act
            var result = _sut.CalculateBasicShipmentCost(parcel);
            //Assert
            Assert.Equal(25, result);
        }
        [Fact]
        public void CalculateBasicShipmentCost_WithWeightGreaterThanLimitSize_ShouldReturnMoreCost()
        {
            //Arrange
            var smallParcel = new Parcel
            {
                Dimension = 5,
                ParcelType = ParcelType.Small,
                Weight = 2
            };
            var mediumParcel = new Parcel
            {
                Dimension = 30,
                ParcelType = ParcelType.Medium,
                Weight = 5
            };
            var largeParcel = new Parcel
            {
                Dimension = 80,
                ParcelType = ParcelType.Large,
                Weight = 10
            };
            var xlargeParcel = new Parcel
            {
                Dimension = 150,
                ParcelType = ParcelType.Xlarge,
                Weight = 15
            };
            var heavyParcel = new Parcel
            {
                Dimension = 300,
                ParcelType = ParcelType.Heavy,
                Weight = 70
            };
            //Act
            var mediumResult = _sut.CalculateBasicShipmentCost(mediumParcel);
            var smallResult = _sut.CalculateBasicShipmentCost(smallParcel);
            var largeResult = _sut.CalculateBasicShipmentCost(largeParcel);
            var xlargeResult = _sut.CalculateBasicShipmentCost(xlargeParcel);
            var heavyResult = _sut.CalculateBasicShipmentCost(heavyParcel);
            //Assert
            Assert.Equal(12, mediumResult);
            Assert.Equal(5, smallResult);
            Assert.Equal(23, largeResult);
            Assert.Equal(35, xlargeResult);
            Assert.Equal(70, heavyResult);
        }
        [Fact]
        public void CalculateOrderCost_With4SmallParcels_ShouldReturnRelatedDiscountAndUpdateTotalCost()
        {
            //Arrange
            var order = new Order
            {
                Parcels = new List<Parcel>
                {
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=5,
                        Weight=1
                    },
                        new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=7,
                        Weight=2
                    },
                           new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=9,
                        Weight=3
                    },
                              new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=4,
                        Weight=1
                    },
                },
                FastSpeed = false
            };
            //Act
            var result = _sut.CalculateOrderCost(order);
            //Assert
            Assert.Equal(15, result.TotalCost);
            Assert.Equal(-1, result.Discount);
        }
        [Fact]
        public void CalculateOrderCost_With3MediumParcels_ShouldReturnRelatedDiscountAndUpdateTotalCost()
        {
            //Arrange
            var order = new Order
            {
                Parcels = new List<Parcel>
                {
                     new Parcel
                    {
                        ParcelType=ParcelType.Medium,
                        Dimension=20,
                        Weight=2
                    },
                        new Parcel
                    {
                        ParcelType=ParcelType.Medium,
                        Dimension=45,
                        Weight=4
                    },
                        new Parcel
                    {
                        ParcelType=ParcelType.Medium,
                        Dimension=35,
                        Weight=3
                    },
                },
                FastSpeed = false
            };
            //Act
            var result = _sut.CalculateOrderCost(order);
            //Assert
            Assert.Equal(18, result.TotalCost);
            Assert.Equal(-1, result.Discount);
        }
        [Fact]
        public void CalculateOrderCost_With5MixedParcels_ShouldReturnRelatedDiscountAndUpdateTotalCost()
        {
            //Arrange
            var order = new Order
            {
                Parcels = new List<Parcel>
                {
                     new Parcel
                    {
                        ParcelType=ParcelType.Medium,
                        Dimension=20,
                        Weight=2
                    },
                        new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=7,
                        Weight=3
                    },
                        new Parcel
                    {
                        ParcelType=ParcelType.Large,
                        Dimension=70,
                        Weight=7
                    },
                        new Parcel
                    {
                        ParcelType=ParcelType.Xlarge,
                        Dimension=120,
                        Weight=12
                    },
                        new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=7,
                        Weight=4
                    },
                },
                FastSpeed = true
            };
            //Act
            var result = _sut.CalculateOrderCost(order);
            //Assert
            Assert.Equal(126, result.TotalCost);
            Assert.Equal(-1, result.Discount);
        }
        [Fact]
        public void CalculateOrderCost_With8SmallParcels_ShouldReturnRelatedDiscountAndUpdateTotalCost()
        {
            //Arrange
            var order = new Order
            {
                Parcels = new List<Parcel>
                {
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=5,
                        Weight=1
                    },
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=7,
                        Weight=2
                    },
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=9,
                        Weight=3
                    },
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=4,
                        Weight=1
                    },
                   new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=5,
                        Weight=1
                    },
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=7,
                        Weight=2
                    },
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=9,
                        Weight=3
                    },
                     new Parcel
                    {
                        ParcelType=ParcelType.Small,
                        Dimension=4,
                        Weight=1
                    },
                },
                FastSpeed = false
            };
            //Act
            var result = _sut.CalculateOrderCost(order);
            //Assert
            Assert.Equal(30, result.TotalCost);
            Assert.Equal(-2, result.Discount);
        }
    }
}
