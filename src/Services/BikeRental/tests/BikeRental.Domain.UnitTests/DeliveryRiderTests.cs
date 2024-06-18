using BikeRental.Domain.Exceptions;
using BikeRental.Domain.Models.DeliveryRiderAggregate;
using BikeRental.Domain.ValueObjects;

namespace BikeRental.Domain.UnitTests
{
    public class DeliveryRiderTests
    {
        [Fact]
        public void CreateDeliveryRider_ValidData_DeliveryRiderCreated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var birthday = new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime();
            var cnpj = CNPJ.Parse("15.535.772/0001-03");

            // Act
            var deliveryRider = new DeliveryRider(userId, "John Doe", birthday, cnpj);

            // Assert
            Assert.Equal(userId, deliveryRider.UserId);
            Assert.Equal("John Doe", deliveryRider.Name);
            Assert.Equal(birthday, deliveryRider.Birthday);
            Assert.Equal(cnpj.Value, deliveryRider.Cnpj.Value);

            Assert.Null(deliveryRider.Cnh);
            Assert.Null(deliveryRider.CurrentBikeId);
            Assert.Null(deliveryRider.CurrentDeliveryRequestId);

            Assert.False(deliveryRider.IsDeleted);

            Assert.NotEmpty(deliveryRider.DomainEvents);
        }

        [Fact]
        public void UpdateCNPJ_ValidCNPJ_CNPJUpdated()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            var cnpj = CNPJ.Parse("11.217.941/0001-06");

            // Act
            deliveryRider.UpdateCNPJ(cnpj);

            // Assert
            Assert.Equal(cnpj.Value, deliveryRider.Cnpj.Value);
        }

        [Fact]
        public void UpdateCNH_ValidCNH_CNHUpdated()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            var cnh = new CNH(Enums.ECNHType.A, "35370788889", "https://google.com");

            // Act
            deliveryRider.UpdateCNH(cnh);

            // Assert
            Assert.Equal(cnh.Number, deliveryRider.Cnh.Number);
            Assert.Equal(cnh.Type, deliveryRider.Cnh.Type);
            Assert.Equal(cnh.Image, deliveryRider.Cnh.Image);
        }

        [Fact]
        public void UpdateBirthday_ValidBirthday_BirthdayUpdated()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            var birthday = new DateTime(1991, 1, 1, 0, 0, 0).ToUniversalTime();

            // Act
            deliveryRider.UpdateBirthday(birthday);

            // Assert
            Assert.Equal(birthday, deliveryRider.Birthday);
        }

        [Fact]
        public void UpdateBirthday_InvalidBirthday_ExceptionThrown()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            var birthday = new DateTime(2010, 1, 1, 0, 0, 0).ToUniversalTime();

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRider.UpdateBirthday(birthday));
        }

        [Fact]
        public void MarkAsDeleted_DeliveryRiderIsNotDeleted_DeliveryRiderMarkedAsDeleted()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));

            // Act
            deliveryRider.MarkAsDeleted();

            // Assert
            Assert.True(deliveryRider.IsDeleted);
            Assert.NotNull(deliveryRider.DeletedAt);
        }

        [Fact]
        public void AttachBike_DeliveryRiderDoesNotHaveBike_BikeAttached()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            var bikeId = 1;

            // Act
            deliveryRider.AttachBike(bikeId);

            // Assert
            Assert.Equal(bikeId, deliveryRider.CurrentBikeId);
        }

        [Fact]
        public void AttachBike_DeliveryRiderHasBike_ExceptionThrown()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            deliveryRider.AttachBike(1);

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRider.AttachBike(2));
        }

        [Fact]
        public void DetachBike_DeliveryRiderHasBike_BikeDetached()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            deliveryRider.AttachBike(1);

            // Act
            deliveryRider.DetachBike();

            // Assert
            Assert.Null(deliveryRider.CurrentBikeId);
        }

        [Fact]
        public void DetachBike_DeliveryRiderDoesNotHaveBike_ExceptionThrown()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRider.DetachBike());
        }

        [Fact]
        public void AcceptDeliveryRequestId_DeliveryRiderDoesNotHaveDeliveryRequest_DeliveryRequestAccepted()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            var deliveryRequestId = Guid.NewGuid();

            // Act
            deliveryRider.AcceptDeliveryRequestId(deliveryRequestId);

            // Assert
            Assert.Equal(deliveryRequestId, deliveryRider.CurrentDeliveryRequestId);
        }

        [Fact]
        public void AcceptDeliveryRequestId_DeliveryRiderHasDeliveryRequest_ExceptionThrown()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            deliveryRider.AcceptDeliveryRequestId(Guid.NewGuid());

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRider.AcceptDeliveryRequestId(Guid.NewGuid()));
        }

        [Fact]
        public void FinishDeliveryRequest_DeliveryRiderHasDeliveryRequest_DeliveryRequestFinished()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));
            deliveryRider.AcceptDeliveryRequestId(Guid.NewGuid());

            // Act
            deliveryRider.FinishDeliveryRequest();

            // Assert
            Assert.Null(deliveryRider.CurrentDeliveryRequestId);
        }

        [Fact]
        public void FinishDeliveryRequest_DeliveryRiderDoesNotHaveDeliveryRequest_ExceptionThrown()
        {
            // Arrange
            var deliveryRider = new DeliveryRider(Guid.NewGuid().ToString(), "John Doe", new DateTime(1990, 1, 1, 0, 0, 0).ToUniversalTime(), CNPJ.Parse("15.535.772/0001-03"));

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRider.FinishDeliveryRequest());
        }

    }
}
