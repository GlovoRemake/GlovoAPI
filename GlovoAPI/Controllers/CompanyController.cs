using Core.Commands.Company;
using Core.Dtos.Company;
using Core.Queries.Company;
using GlovoAPI.Policy.Attributes;
using GlovoAPI.Policy.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Roles = "Owner, Admin, Support")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] RequestsPagedDto dto)
        {
            var result = await _mediator.Send(new GetAllCompanyRequestsQuery(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize(Roles = "Owner, Admin, Support")]
        [HttpPost("approval")]
        public async Task<IActionResult> ApprovalRequest([FromQuery] ApprovalCompanyDto dto)
        {
            var result = await _mediator.Send(new ApprovalRequestCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpPost("test/{companyId:Guid}")]
        public async Task<IActionResult> test(Guid companyId, [FromBody] string data)
        {
            return Ok(new { CompanyId = companyId, Data = data });
        }
    }
}
