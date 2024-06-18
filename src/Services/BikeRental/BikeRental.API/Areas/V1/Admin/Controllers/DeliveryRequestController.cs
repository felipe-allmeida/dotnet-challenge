using BikeRental.API.DTOs.V1.Requests;
using BikeRental.API.Infrastructure.Security;
using BikeRental.Application.Commands.V1.Admin.CreateDeliveryRequest;
using BikeRental.Application.DTOs.V1;
using BikeRental.Application.Queries.V1.Admin.GetDeliveryRequestNotifications;
using BikeRental.Application.Queries.V1.Admin.GetDeliveryRequests;
using BuildingBlocks.Common;
using BuildingBlocks.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.API.Areas.V1.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [Route("api/v1/[area]/delivery-requests")]
    public class DeliveryRequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public DeliveryRequestController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        [Authorize(Policy = Policies.AdminRead)]
        public async Task<ActionResult<PaginatedItem<DeliveryRequestDto>>> Get([FromQuery] PaginatedDto query)
        {
            var result = await _mediator.Send(new GetDeliveryRequestsQuery
            {
                Skip = query.Skip,
                Take = query.Take
            });

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.AdminWrite)]
        public async Task<IActionResult> CreateDeliveryRequest([FromBody] CreateDeliveryRequestDto body)
        {
            var result = await _mediator.Send(new CreateDeliveryRequestCommand
            {
                PriceCents = body.PriceCents
            });

            return CreatedAtAction(nameof(CreateDeliveryRequest), new { id = result.Id }, new { id = result.Id });
        }

        [HttpGet("{id}/notifications")]
        [Authorize(Policy = Policies.AdminRead)]
        public async Task<ActionResult<PaginatedItem<DeliveryRequestNotificationDto>>> GetDeliveryRequestNotifications([FromRoute] Guid id, [FromQuery] PaginatedDto query)
        {
            var result = await _mediator.Send(new GetDeliveryRequestNotificationsQuery
            {
                DeliveryRequestId = id,
                Skip = query.Skip,
                Take = query.Take
            });
            return Ok(result);
        }
    }
}
