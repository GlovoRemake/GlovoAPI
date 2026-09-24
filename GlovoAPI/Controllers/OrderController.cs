using Core.Commands.Account.Order;
using Core.Dtos.Account.Order;
using Core.Queries.Account.Cart;
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

            return Ok(new { result.IsSuccess, result = true });
        }
    }
}
