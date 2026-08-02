using Core.Commands.Company;
using Core.Commands.Partner;
using Core.Dtos.Company;
using Core.Dtos.Company.Category;
using Core.Queries.Company;
using Core.Queries.Company.Category;
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

        [HttpPost("add")]
        public async Task<IActionResult> AddCompanyCategory([FromQuery] ApprovalCompanyDto dto)
        {
            var result = await _mediator.Send(new ApprovalRequestCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditCompanyCategory([FromQuery] ApprovalCompanyDto dto)
        {
            var result = await _mediator.Send(new ApprovalRequestCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveCompanyCategory([FromQuery] ApprovalCompanyDto dto)
        {
            var result = await _mediator.Send(new ApprovalRequestCommand(dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
    }
}
