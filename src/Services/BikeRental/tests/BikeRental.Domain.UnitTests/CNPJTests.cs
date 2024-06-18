using BikeRental.Domain.Exceptions;
using BikeRental.Domain.ValueObjects;

namespace BikeRental.Domain.UnitTests
{
    public class CNPJTests
    {
        [Theory]
        [InlineData("15.535.772/0001-03")]
        [InlineData("15.535.7720001-03")]
        [InlineData("15535772000103")]
            
        public void Parse_ValidCNPJ_CNPJCreated(string data)
        {
            // Arrange
            var cleanData = data.Replace(".", "").Replace("/", "").Replace("-", "");

            // Act
            var cnpj = CNPJ.Parse(data);

            // Assert
            Assert.Equal(cleanData, cnpj.Value.ToString());
        }

        [Theory]
        [InlineData("00.000.000/0000-01")]
        [InlineData("")]
        [InlineData(null)]
        public void Parse_InvalidCNPJ_ExceptionThrown(string data)
        {
            // Act
            // Assert
            Assert.Throws<DomainException>(() => CNPJ.Parse(data));
        }
    }
}
