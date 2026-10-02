using Core.Commands.Account.Order;
using Core.Commands.Courier;
using Core.Dtos.Account.Order;
using Core.Queries.Account.Cart;
using Core.Queries.Account.Order;
using Core.Queries.Courier;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetCarts([FromBody] ConfirmOrderDto dto)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new ConfirmOrderCommand(res ? id : Guid.Empty, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [Authorize]
        [HttpGet("order-for-courier")]
        public async Task<IActionResult> GetOrdersForCourier(double latitude, double longitude)
        {
            var result = await _mediator.Send(new GetOrdersForCouriersQuery(latitude, longitude));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }


        [Authorize]
        [HttpPost("courier-accept")]
        public async Task<IActionResult> CourierAccept(int orderId)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new CourierAcceptOrderCommand(orderId, res ? id : Guid.Empty));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize]
        [HttpGet("courier-order")]
        public async Task<IActionResult> CourierActiveOrder()
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new GetCourierCurrentOrderQuery(res ? id : Guid.Empty));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize]
        [HttpPost("confirm-delivery")]
        public async Task<IActionResult> ConfirmDelivery(int orderId)
        {
            var result = await _mediator.Send(new ConfirmDeliveryOrderCommand(orderId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [Authorize]
        [HttpGet("my-active-orders")]
        public async Task<IActionResult> GetActiveOrders()
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new GetActiveOrdersQuery(res ? id : Guid.Empty));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
    }
}
