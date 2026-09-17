using Core.Commands.Account;
using Core.Commands.Account.Address;
using Core.Dtos.Account.Address;
using Core.Queries.Account.Address;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new GetUserAdressesQuery(res ? id : Guid.Empty));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressDto dto)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var id);
            var result = await _mediator.Send(new AddAddressCommand(res ? id : Guid.Empty, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var res = Guid.TryParse(User.FindFirst("id")?.Value, out var userId);
            var result = await _mediator.Send(new DeleteAddressCommand(res ? userId : Guid.Empty, id));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
    }
}
