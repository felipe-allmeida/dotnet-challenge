using BikeRental.Domain.Exceptions;
using BikeRental.Domain.Models.DeliveryRequestNotificationAggregate;

namespace BikeRental.Domain.UnitTests
{
    public class DeliveryRequestNotificationTests
    {
        [Fact]
        public void CreateDeliveryRequestNotification_ValidData_DeliveryRequestNotificationCreated()
        {
            // Arrange
            var deliveryRequestId = Guid.NewGuid();
            var deliveryRiderId = 1;

            // Act
            var deliveryRequestNotification = new DeliveryRequestNotification(deliveryRequestId, deliveryRiderId);

            // Assert
            Assert.Equal(deliveryRequestId, deliveryRequestNotification.DeliveryRequestId);
            Assert.Equal(deliveryRiderId, deliveryRequestNotification.DeliveryRiderId);
            Assert.Equal(Enums.EDeliveryRequestNotificationStatus.Pending, deliveryRequestNotification.Status);

            Assert.NotEmpty(deliveryRequestNotification.DomainEvents);
        }

        [Fact]
        public void MarkAsConsumed_DeliveryRequestNotificationIsNotConsumed_DeliveryRequestNotificationMarkedAsConsumed()
        {
            // Arrange
            var deliveryRequestNotification = new DeliveryRequestNotification(Guid.NewGuid(), 1);

            // Act
            deliveryRequestNotification.MarkAsConsumed();

            // Assert
            Assert.Equal(Enums.EDeliveryRequestNotificationStatus.Consumed, deliveryRequestNotification.Status);
        }

        [Fact]
        public void MarkAsConsumed_DeliveryRequestNotificationIsConsumed_ExceptionThrown()
        {
            // Arrange
            var deliveryRequestNotification = new DeliveryRequestNotification(Guid.NewGuid(), 1);
            deliveryRequestNotification.MarkAsConsumed();

            // Act
            // Assert
            Assert.Throws<DomainException>(() => deliveryRequestNotification.MarkAsConsumed());
        }
    }
}
