using Core.Commands.Company.Product;
using Core.Dtos.Company.Product;
using Core.Queries.Company.Affiliate;
using Core.Queries.Company.Product;
using GlovoAPI.Policy.Attributes;
using GlovoAPI.Policy.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers
{
    [Route("api/company/product")]
    [ApiController]
    public class CompanyProductController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("create/{companyId:Guid}/{categoryId:int}")]
        public async Task<IActionResult> GetProducts(Guid companyId, int categoryId)
        {
            var result = await _mediator.Send(new GetProductsQuery(companyId, categoryId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPost("create/{companyId:Guid}")]
        public async Task<IActionResult> CreateProduct(Guid companyId, [FromForm] CreateProductDto dto)
        {
            var result = await _mediator.Send(new CreateProductCommand(companyId, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPut("update/{productId:int}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] UpdateProductDto dto)
        {
            var result = await _mediator.Send(new UpdateProductCommand(productId, dto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpDelete("delete/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _mediator.Send(new DeleteProductCommand(productId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, result.Value });
        }
    }
}
