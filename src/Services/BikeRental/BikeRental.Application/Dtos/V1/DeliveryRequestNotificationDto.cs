using BikeRental.Domain.Enums;

namespace BikeRental.Application.DTOs.V1
{
    public record DeliveryRequestNotificationDto
    {
        public Guid Id { get; init; }
        public EDeliveryRequestNotificationStatus Status { get; init; }
        public Guid DeliveryRequestId { get; init; }
        public long DeliveryRiderId { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}
