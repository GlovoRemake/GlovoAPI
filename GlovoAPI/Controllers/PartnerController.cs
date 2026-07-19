using Core.Commands.Partner;
using Core.Dtos.Account;
using Core.Dtos.Partner;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Refister([FromBody] PartnerRegisterDto model)
        {
            var result = await _mediator.Send(new PartnerRegisterCommand(model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, Value = true });
        }
        
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> Verify([FromBody] VerifyCodeDto model)
        {
            var result = await _mediator.Send(new VerifyPartnerCodeCommand(model));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var result = await _mediator.Send(new PartnerLoginCommand(model.Email, model.Password));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
        
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            var result = await _mediator.Send(new PartnerRefreshTokenCommand(refreshRequest.RefreshToken));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
    }
}