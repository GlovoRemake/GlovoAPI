using Core.Commands.Company.Category;
using Core.Commands.Company.Product.Additional;
using Core.Dtos.Company.Category;
using Core.Dtos.Company.Product.AdditionalGroup;
using Core.Queries.Company.Affiliate;
using Core.Queries.Company.Product.Additional;
using GlovoAPI.Policy.Attributes;
using GlovoAPI.Policy.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalController(IMediator _mediator) : ControllerBase
    {
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpGet("all/{companyId:Guid}/{productId:int}")]
        public async Task<IActionResult> GetAdditionals(Guid companyId, int productId)
        {
            var result = await _mediator.Send(new GetAdditionalsQuery(productId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPost("create/{companyId:Guid}/{productId:int}")]
        public async Task<IActionResult> Create(Guid companyId, int productId, CreateAdditionalGroupDto dto)
        {
            var result = await _mediator.Send(new CreateAdditionalCommand(productId, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPut("reorder/{companyId:Guid}/{productId:int}")]
        public async Task<IActionResult> ReorderCompanyCategories(Guid companyId, int productId, ReorderAdditionalGroupDto dto)
        {
            var result = await _mediator.Send(new ReorderAdditionalCommand(productId, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }
    }
}
