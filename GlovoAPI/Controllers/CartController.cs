using Core.Commands.Account.Cart;
using Core.Dtos.Account.Cart;
using Core.Queries.Account.Address;
using Core.Queries.Account.Cart;
using Core.Queries.Company.Product.Additional;
using GlovoAPI.Policy.Attributes;
using GlovoAPI.Policy.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCarts()
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new GetUserCartsQuery(res ? id : Guid.Empty));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new AddToCartCommand(res ? id : Guid.Empty, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [Authorize]
        [HttpPut("{cartId:int}")]
        public async Task<IActionResult> UpdateCart([FromQuery] int cartId, [FromBody] UpdateCartDto dto)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new UpdateCartCommand(res ? id : Guid.Empty, cartId, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [Authorize]
        [HttpDelete("{cartId:int}")]
        public async Task<IActionResult> DeleteCart ([FromQuery] int cartId)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new DeleteFromCartCommand(res ? id : Guid.Empty, cartId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [Authorize]
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAllCart()
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new DeleteAllCartCommand(res ? id : Guid.Empty));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
    }
}
