using Core.Commands.Promocodes;
using Core.Dtos.Company;
using Core.Dtos.Promocods;
using Core.Queries.Promocodes;
using GlovoAPI.Policy.Attributes;
using GlovoAPI.Policy.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromocodeController(IMediator _mediator) : ControllerBase
{
    [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
    [HttpPost("create/{companyId:Guid}")]
    public async Task<IActionResult> CreatePromocode([FromRoute] Guid companyId, [FromBody] CreatePromocodeDto dto)
    {
        var result = await _mediator.Send(new CreatePromocodeCommand(companyId, dto));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess });
    }

    [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
    [HttpGet("get/{compnayId:Guid}/{id:int}")]
    public async Task<IActionResult> GetPromocode(int id)
    {
        var result = await _mediator.Send(new GetPromocodeQuery(id));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess, result.Value });
    }

    [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
    [HttpGet("getAll/{companyId:Guid}")]
    public async Task<IActionResult> GetAllPromocodesByCompany(Guid companyId)
    {
        var result = await _mediator.Send(new GetAllPromocodesQuery(companyId));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess, result.Value });
    }

    [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
    [HttpPut("update/{companyId:Guid}")]
    public async Task<IActionResult> UpdatePromocode([FromRoute] Guid companyId, [FromBody] UpdatePromocodeDto dto)
    {
        var result = await _mediator.Send(new UpdatePromocodeCommand(companyId, dto));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess });
    }

    [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
    [HttpDelete("delete/{companyId:Guid}/{id:int}")]
    public async Task<IActionResult> DeletePromocode(int id)
    {
        var result = await _mediator.Send(new DeletePromocodeCommand(id));

        if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

        return Ok(new { result.IsSuccess });
    }
}
