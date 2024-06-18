using BikeRental.Application.DTOs.V1;
using BikeRental.Application.Extensions;
using BikeRental.Domain.Models.DeliveryRequestNotificationAggregate;
using BuildingBlocks.Common;
using MediatR;

namespace BikeRental.Application.Queries.V1.Admin.GetDeliveryRequestNotifications
{
    public class GetDeliveryRequestNotificationsQueryHandler : IRequestHandler<GetDeliveryRequestNotificationsQuery, PaginatedItem<DeliveryRequestNotificationDto>>
    {
        private readonly IDeliveryRequestNotificationQueryRepository _queryRepository;

        public GetDeliveryRequestNotificationsQueryHandler(IDeliveryRequestNotificationQueryRepository queryRepository)
        {
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
        }

        public async Task<PaginatedItem<DeliveryRequestNotificationDto>> Handle(GetDeliveryRequestNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _queryRepository.GetAll()
                .Where(x => x.DeliveryRequestId == request.DeliveryRequestId)
                .Select(x => new DeliveryRequestNotificationDto
                {
                    Id = x.Id,
                    Status = x.Status,
                    DeliveryRequestId = x.DeliveryRequestId,
                    DeliveryRiderId = x.DeliveryRiderId,
                    CreatedAt = x.CreatedAt
                }).OrderByDescending(x => x.CreatedAt)
                .PaginateAsync(request.Skip, request.Take);
        }
    }
}
