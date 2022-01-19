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
        public void CalculateShipment_WithSmallType_ShouldReturn3()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 5,
                ParcelType = ParcelType.Small
            };
            //Act
            var result = _sut.CalculateShipment(parcel);
            //Assert
            Assert.Equal(3, result);
        }
        [Fact]
        public void CalculateShipment_WithMediumType_ShouldReturn8()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 40,
                ParcelType = ParcelType.Medium
            };
            //Act
            var result = _sut.CalculateShipment(parcel);
            //Assert
            Assert.Equal(8, result);
        }
        [Fact]
        public void CalculateShipment_WithLarge_ShouldReturn15()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 80,
                ParcelType = ParcelType.Large
            };
            //Act
            var result = _sut.CalculateShipment(parcel);
            //Assert
            Assert.Equal(15, result);
        }
        [Fact]
        public void CalculateShipment_WithXLargeType_ShouldReturn25()
        {
            //Arrange
            var parcel = new Parcel
            {
                Dimension = 300,
                ParcelType = ParcelType.Xlarge
            };
            //Act
            var result = _sut.CalculateShipment(parcel);
            //Assert
            Assert.Equal(25, result);
        }
    }
}
