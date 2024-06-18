using BikeRental.Domain.ValueObjects;

namespace BikeRental.Domain.UnitTests
{
    public class CNHTests
    {
        [Fact]
        public void CreateCNH_ValidData_Created()
        {
            // Arrange
            var type = Enums.ECNHType.A;
            var number = "35370788889";
            var image = "https://google.com";

            // Act
            var cnh = new CNH(type, number, image);

            // Assert
            Assert.Equal(type, cnh.Type);
            Assert.Equal(number, cnh.Number);
            Assert.Equal(image, cnh.Image);
        }
    }
}
