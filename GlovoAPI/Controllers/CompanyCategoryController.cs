using Core.Commands.Company;
using Core.Commands.Company.Category;
using Core.Commands.Partner;
using Core.Dtos.Company;
using Core.Dtos.Company.Category;
using Core.Queries.Company;
using Core.Queries.Company.Category;
using GlovoAPI.Policy.Attributes;
using GlovoAPI.Policy.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/company/category")]
    [ApiController]
    public class CompanyCategoryController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCompanyCategories([FromQuery] GetAllCategoriesDto query)
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery(query));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPost("add")]
        public async Task<IActionResult> AddCompanyCategory([FromBody] AddCategoryDto dto)
        {
            var result = await _mediator.Send(new AddCategoryCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPut("edit")]
        public async Task<IActionResult> EditCompanyCategory([FromBody] UpdateCategoryDto dto)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveCompanyCategory([FromBody] DeleteCategoryDto dto)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPut("reorder")]
        public async Task<IActionResult> ReorderCompanyCategories([FromBody] ReorderCategoryDto dto)
        {
            var result = await _mediator.Send(new ReorderCategoryCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess });
        }
    }
}
