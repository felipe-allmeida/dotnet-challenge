using BikeRental.Application.DTOs.V1;
using BikeRental.Application.Extensions;
using BikeRental.Domain.Models.DeliveryRequestAggregate;
using BuildingBlocks.Common;
using MediatR;

namespace BikeRental.Application.Queries.V1.Admin.GetDeliveryRequests
{
    public class GetDeliveryRequestsQueryHandler : IRequestHandler<GetDeliveryRequestsQuery, PaginatedItem<DeliveryRequestDto>>
    {
        private readonly IDeliveryRequestQueryRepository _queryRepository;

        public GetDeliveryRequestsQueryHandler(IDeliveryRequestQueryRepository queryRepository)
        {
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
        }

        public async Task<PaginatedItem<DeliveryRequestDto>> Handle(GetDeliveryRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _queryRepository.GetAll()
                .Select(x => new DeliveryRequestDto
                {
                    Id = x.Id,
                    Status = x.Status,
                    DeliveryRiderId = x.DeliveryRiderId,
                    PriceCents = x.PriceCents,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .OrderByDescending(x => x.CreatedAt)
                .PaginateAsync(request.Skip, request.Take);
        }
    }
}
