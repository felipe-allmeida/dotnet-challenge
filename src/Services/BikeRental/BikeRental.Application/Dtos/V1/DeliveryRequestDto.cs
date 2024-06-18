using BikeRental.Domain.Enums;

namespace BikeRental.Application.DTOs.V1
{
    public record DeliveryRequestDto
    {
        public Guid Id { get; init; }
        public long? DeliveryRiderId { get; init; }
        public EDeliveryRequestStatus Status { get; init; }
        public int PriceCents { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; init; }
    }
}
