using BikeRental.API.DTOs.V1.Requests;
using BikeRental.API.Infrastructure.Security;
using BikeRental.Application.Commands.V1.User.CreateDeliveryRider;
using BikeRental.Application.Commands.V1.User.UpdateDeliveryRiderCnh;
using BikeRental.Application.DTOs.V1;
using BikeRental.Application.Queries.V1.User.GetDeliveryRider;
using BuildingBlocks.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.API.Areas.V1.User.Controllers
{
    
    [Area("user")]
    [ApiController]
    [Route("api/v1/[area]/delivery-riders")]
    public class DeliveryRidersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly LinkGenerator _linkGenerator;

        public DeliveryRidersController(IMediator mediator, IIdentityService identityService, LinkGenerator linkGenerator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
        }

        [HttpGet("me")]
        [Authorize(Policy = Policies.DeliveryRiderRead)]
        public async Task<ActionResult<DeliveryRiderDto>> GetDeliveryRider()
        {
            var result = await _mediator.Send(new GetDeliveryRiderQuery { UserId = _identityService.GetUserId() });

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.DeliveryRiderWrite)]
        public async Task<IActionResult> CreateDeliveryRider([FromBody] CreateDeliveryRiderDto body)
        {
            var result = await _mediator.Send(new CreateDeliveryRiderCommand
            {
                UserId = _identityService.GetUserId(),
                Name = body.Name,
                Cnpj = body.Cnpj,
                Birthday = body.Birthday
            });

            return CreatedAtAction(nameof(CreateDeliveryRider), new { id = result.Id }, new { id = result.Id });
        }

        [HttpPatch("me/cnh")]
        [Authorize(Policy = Policies.DeliveryRiderWrite)]
        public async Task<IActionResult> UpdateCnh([FromForm] UpdateDeliveryRiderCnhDto body)
        {
            await _mediator.Send(new UpdateDeliveryRiderCnhCommand
            {
                UserId = _identityService.GetUserId(),
                CnhType = body.Type,
                CnhNumber = body.Number,
                CnhImage = body.File
            });

            return NoContent();
        }


    }
}
