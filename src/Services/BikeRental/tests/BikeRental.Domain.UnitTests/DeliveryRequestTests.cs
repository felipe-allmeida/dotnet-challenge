using BikeRental.Domain.Exceptions;
using BikeRental.Domain.Models.DeliveryRequestAggregate;

namespace BikeRental.Domain.UnitTests
{
    public class DeliveryRequestTests
    {
        [Fact]
        public void CreateDeliveryRequest_ValidData_DeliveryRequestCreated()
        {
            // Arrange
            var priceCents = 1000;

            // Act
            var deliveryRequest = new DeliveryRequest(priceCents);

            // Assert
            Assert.Equal(priceCents, deliveryRequest.PriceCents);
            Assert.Equal(Enums.EDeliveryRequestStatus.Avaiable, deliveryRequest.Status);            

            Assert.NotEmpty(deliveryRequest.DomainEvents);
        }

        [Fact]
        public void MarkAsAccepted_DeliveryRequestIsAvailable_DeliveryRequestMarkedAsAccepted()
        {
            // Arrange
            var deliveryRequest = new DeliveryRequest(1000);
            var deliveryRiderId = 1;

            // Act
            deliveryRequest.MarkAsAccepted(deliveryRiderId);

            // Assert
            Assert.Equal(deliveryRiderId, deliveryRequest.DeliveryRiderId);
            Assert.Equal(Enums.EDeliveryRequestStatus.Accepted, deliveryRequest.Status);
        }

        [Fact]
        public void MarkAsAccepted_DeliveryRequestIsNotAvailable_ExceptionThrown()
        {
            // Arrange
            var deliveryRequest = new DeliveryRequest(1000);
            deliveryRequest.MarkAsAccepted(1);

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRequest.MarkAsAccepted(2));
        }

        [Fact]
        public void MarkAsDelivered_DeliveryRequestIsAccepted_DeliveryRequestMarkedAsDelivered()
        {
            // Arrange
            var deliveryRequest = new DeliveryRequest(1000);
            deliveryRequest.MarkAsAccepted(1);

            // Act
            deliveryRequest.MarkAsDelivered();

            // Assert
            Assert.Equal(Enums.EDeliveryRequestStatus.Delivered, deliveryRequest.Status);
        }

        [Fact]
        public void MarkAsDelivered_DeliveryRequestIsNotAccepted_ExceptionThrown()
        {
            // Arrange
            var deliveryRequest = new DeliveryRequest(1000);

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRequest.MarkAsDelivered());
        }
    }
}
