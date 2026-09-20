using Core.Queries.Admin;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("regions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var result = await _mediator.Send(new GetAllRegionsQuery());

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
    }
}
