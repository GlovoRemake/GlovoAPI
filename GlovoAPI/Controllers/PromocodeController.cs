using Core.Commands.Promocodes;
using Core.Dtos.Promocods;
using Core.Queries.Promocodes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromocodeController(IMediator _mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreatePromocode([FromBody] CreatePromocodeDto dto)
    {
        var result = await _mediator.Send(new CreatePromocodeCommand(dto));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess });
    }
    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetPromocode(int id)
    {
        var result = await _mediator.Send(new GetPromocodeQuery(id));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess, result.Value });
    }

    [HttpGet("getAll/{companyId:Guid}")]
    public async Task<IActionResult> GetAllPromocodesByCompany(Guid companyId)
    {
        var result = await _mediator.Send(new GetAllPromocodesQuery(companyId));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess, result.Value });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePromocode([FromBody] UpdatePromocodeDto dto)
    {
        var result = await _mediator.Send(new UpdatePromocodeCommand(dto));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeletePromocode(int id)
    {
        var result = await _mediator.Send(new DeletePromocodeCommand(id));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess });
    }
}
