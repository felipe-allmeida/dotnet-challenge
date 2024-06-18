using BikeRental.Application.DTOs.V1;
using BuildingBlocks.Common;
using MediatR;

namespace BikeRental.Application.Queries.V1.Admin.GetDeliveryRequestNotifications
{
    public record GetDeliveryRequestNotificationsQuery : IRequest<PaginatedItem<DeliveryRequestNotificationDto>>
    {
        public Guid DeliveryRequestId { get; init; }
        public int Skip { get; init; } = 0;
        public int Take { get; init; } = 10;
    }
}
