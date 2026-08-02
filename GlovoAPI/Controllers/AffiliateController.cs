using Core.Commands.Company;
using Core.Commands.Company.Affiliate;
using Core.Dtos.Company.Affiliate;
using Core.Queries.Company.Affiliate;
using GlovoAPI.Policy.Attributes;
using GlovoAPI.Policy.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/company/affiliate")]
    [ApiController]
    public class AffiliateController(IMediator _mediator) : ControllerBase
    {
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpGet("all/{companyId:Guid}")]
        public async Task<IActionResult> GetAllAffiliates(Guid companyId, int pageNumber, int pageSize)
        {
            var result = await _mediator.Send(new GetAllAffiliatesQuery(companyId, pageNumber, pageSize));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [Authorize]
        [HttpGet("by-region/{companyId:Guid}")]
        public async Task<IActionResult> GetAllAffiliatesByRegion(Guid companyId, int cityId)
        {
            var result = await _mediator.Send(new GetAllAffiliatesByRegionQuery(companyId, cityId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPost("add/{companyId:Guid}")]
        public async Task<IActionResult> CreateAffiliate(Guid companyId, [FromBody] CreateAffiliateDto dto)
        {
            var result = await _mediator.Send(new CreateAffiliateCommand(companyId, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpPut("update/{affiliateId:Guid}")]
        public async Task<IActionResult> UpdateAffiliate(Guid affiliateId, [FromBody] UpdateAffiliateDto dto)
        {
            var result = await _mediator.Send(new UpdateAffiliateCommand(affiliateId, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpDelete("delete/{affiliateId:Guid}")]
        public async Task<IActionResult> DeleteAffiliate(Guid affiliateId)
        {
            var result = await _mediator.Send(new DeleteAffiliateCommand(affiliateId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
    }
}
