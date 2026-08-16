using Core.Commands.Company;
using Core.Commands.Company.Affiliate;
using Core.Commands.Company.Affiliate.Category;
using Core.Dtos.Company.Affiliate;
using Core.Queries.Company.Affiliate;
using Core.Queries.Company.Affiliate.Category;
using Core.Queries.Company.Affiliate.Product;
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




        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpPost("manager/{affiliateId:Guid}")]
        public async Task<IActionResult> AddManager(Guid affiliateId, OperationAffiliateUserDto partnerDto)
        {
            var result = await _mediator.Send(new AddManagerCommand(affiliateId, partnerDto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner)]
        [HttpDelete("manager/{affiliateId:Guid}")]
        public async Task<IActionResult> RemoveManager(Guid affiliateId, OperationAffiliateUserDto partnerDto)
        {
            var result = await _mediator.Send(new RemoveManagerCommand(affiliateId, partnerDto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }


        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpPost("employee/{affiliateId:Guid}")]
        public async Task<IActionResult> AddEmployee(Guid affiliateId, OperationAffiliateUserDto partnerDto)
        {
            var result = await _mediator.Send(new AddEmployeeCommand(affiliateId, partnerDto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }

        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpDelete("employee/{affiliateId:Guid}")]
        public async Task<IActionResult> RemoveEmployee(Guid affiliateId, OperationAffiliateUserDto partnerDto)
        {
            var result = await _mediator.Send(new RemoveEmployeeCommand(affiliateId, partnerDto));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
        
        
        
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpGet("categories/{affiliateId:Guid}")]
        public async Task<IActionResult> GetAffiliateCategories(Guid affiliateId)
        {
            var result = await _mediator.Send(new GetAffiliateCategoriesQuery(affiliateId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = result.Value });
        }
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpPost("categories/{affiliateId:Guid}/{categoryId:int}")]
        public async Task<IActionResult> AddCategory(Guid affiliateId, int categoryId)
        {
            var result = await _mediator.Send(new AddAffiliateCategoryCommand(affiliateId, categoryId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpDelete("categories/{affiliateId:Guid}/{categoryId:int}")]
        public async Task<IActionResult> RemoveCategory(Guid affiliateId, int categoryId)
        {
            var result = await _mediator.Send(new RemoveAffiliateCategoryCommand(affiliateId, categoryId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
        
        
        
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpGet("products/{affiliateId:Guid}")]
        public async Task<IActionResult> GetAffiliateProducts(Guid affiliateId)
        {
            var result = await _mediator.Send(new GetAffiliateProductsQuery(affiliateId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = result.Value });
        }
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpPost("products/{affiliateId:Guid}/{productId:int}")]
        public async Task<IActionResult> AddProduct(Guid affiliateId, int productId)
        {
            var result = await _mediator.Send(new AddAffiliateProductCommand(affiliateId, productId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpDelete("products/{affiliateId:Guid}/{productId:int}")]
        public async Task<IActionResult> RemoveProduct(Guid affiliateId, int productId)
        {
            var result = await _mediator.Send(new RemoveAffiliateProductCommand(affiliateId, productId));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
        
        [PartnerAuthorize(PartnerRolesEnum.CompanyOwner, PartnerRolesEnum.AffiliateManager)]
        [HttpPatch("products/{affiliateId:Guid}/{productId:int}")]
        public async Task<IActionResult> ChangeProductAvailability(Guid affiliateId, int productId, [FromBody] bool isAvailable)
        {
            var result = await _mediator.Send(new ChangeProductAvailabilityCommand(affiliateId, productId, isAvailable));

            if (!result.IsSuccess) return BadRequest(new { result.IsSuccess, result.Errors });

            return Ok(new { result.IsSuccess, value = true });
        }
    }
}
