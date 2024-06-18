using BikeRental.Domain.Exceptions;
using BikeRental.Domain.Models.BikeAggregate;

namespace BikeRental.Domain.UnitTests
{
    public class BikeTests
    {
        [Fact]
        public void CreateBike_ValidData_BikeCreated()
        {
            // Arrange
            var plate = "ABCD123";
            var year = 2021;
            var model = "Model";

            // Act
            var bike = new Bike(plate, year, model);

            // Assert
            Assert.Equal(plate, bike.Plate);
            Assert.Equal(year, bike.Year);
            Assert.Equal(model, bike.Model);
        }

        [Fact]
        public void CreateBike_InvalidPlate_ExceptionThrown()
        {
            // Arrange
            var plate = "ABCD1234";
            var year = 2021;
            var model = "Model";

            // Act
            // Assert
            Assert.Throws<DomainException>(() => new Bike(plate, year, model));
        }

        [Fact]
        public void UpdatePlate_ValidPlate_PlateUpdated()
        {
            // Arrange
            var bike = new Bike("ABCD123", 2021, "Model");

            // Act
            bike.UpdatePlate("ABCD321");

            // Assert
            Assert.Equal("ABCD321", bike.Plate);
        }

        [Fact]
        public void UpdatePlate_InvalidPlate_ExceptionThrown()
        {
            // Arrange
            var bike = new Bike("ABCD123", 2021, "Model");

            // Act
            // Assert
            Assert.Throws<DomainException>(() => bike.UpdatePlate("ABCD1234"));
        }

        [Fact]
        public void MarkAsDeleted_BikeIsNotDeleted_BikeMarkedAsDeleted()
        {
            // Arrange
            var bike = new Bike("ABCD123", 2021, "Model");

            // Act
            bike.MarkAsDeleted();

            // Assert
            Assert.True(bike.IsDeleted);
            Assert.NotNull(bike.DeletedAt);
        }
    }
}
