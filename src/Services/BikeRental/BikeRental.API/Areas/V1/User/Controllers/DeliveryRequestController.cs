using BikeRental.API.Infrastructure.Security;
using BikeRental.Application.Commands.V1.User.AcceptDeliveryRequest;
using BikeRental.Application.Commands.V1.User.CompleteDeliveryRequest;
using BuildingBlocks.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.API.Areas.V1.User.Controllers
{
    [Area("user")]
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

        [HttpPost("{id}/accept")]
        [Authorize(Policy = Policies.DeliveryRiderWrite)]
        public async Task<IActionResult> AcceptDeliveryRequest([FromRoute] Guid id)
        {
            await _mediator.Send(new AcceptDeliveryRequestCommand
            {
                DeliveryRequestId = id,
                UserId = _identityService.GetUserId()
            });

            return NoContent();
        }

        [HttpPost("{id}/finish")]
        [Authorize(Policy = Policies.DeliveryRiderWrite)]
        public async Task<IActionResult> CompleteDeliveryRequest([FromRoute] Guid id)
        {
            await _mediator.Send(new CompleteDeliveryRequestCommand
            {
                UserId = _identityService.GetUserId()
            });

            return NoContent();
        }

    }
}
