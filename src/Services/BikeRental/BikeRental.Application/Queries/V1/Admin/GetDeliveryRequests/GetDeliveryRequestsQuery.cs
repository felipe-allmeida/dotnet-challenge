using BikeRental.Application.DTOs.V1;
using BuildingBlocks.Common;
using MediatR;

namespace BikeRental.Application.Queries.V1.Admin.GetDeliveryRequests
{
    public record GetDeliveryRequestsQuery : IRequest<PaginatedItem<DeliveryRequestDto>>
    {
        public int Skip { get; init; } = 0;
        public int Take { get; init; } = 10;
    }
}
