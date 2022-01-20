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
        public void CalculateShipmentCost_WithFastSpeedSetToTrue_ShouldReturnDoubleCost()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 5,
                ParcelType = ParcelType.Small,
                FastSpeed = true,
            };
            //Act
            var result = _sut.CalculateShipmentCost(parcel);
            //Assert
            Assert.Equal(6, result);
        }
        [Fact]
        public void CalculateShipmentCost_WithFastSpeedSetToFalse_ShouldReturnActualCost()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 30,
                ParcelType = ParcelType.Medium,
                FastSpeed = false,
            };
            //Act
            var result = _sut.CalculateShipmentCost(parcel);
            //Assert
            Assert.Equal(8, result);
        }
        [Fact]
        public void CalculateShipmentCost_WithWeightGreaterThanLimitSize_ShouldReturnMoreCost()
        {
            //Arrange
            var smallParcel = new Parcel
            {
                Dimension = 5,
                ParcelType = ParcelType.Small,
                FastSpeed = false,
                Weight = 2
            };
            var mediumParcel = new Parcel
            {
                Dimension = 30,
                ParcelType = ParcelType.Medium,
                FastSpeed = false,
                Weight = 5
            };
            var largeParcel = new Parcel
            {
                Dimension = 80,
                ParcelType = ParcelType.Large,
                FastSpeed = false,
                Weight = 10
            };
            var xlargeParcel = new Parcel
            {
                Dimension = 150,
                ParcelType = ParcelType.Xlarge,
                FastSpeed = false,
                Weight = 15
            };
            var heavyParcel = new Parcel
            {
                Dimension = 300,
                ParcelType = ParcelType.Heavy,
                FastSpeed = true,
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
    }
}
