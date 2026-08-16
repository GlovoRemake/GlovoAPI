using Core.Dtos.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Dtos.Company.Category;
using Core.Dtos.Company.Product;

namespace Core.Interfaces;

public interface IAffiliateService
{
    Task<PagedAffiliatesDto> GetAllAffiliatesAsync(Guid companyId, int pageNumber, int pageSize);
    Task<AffiliateDto[]> GetAllAffiliatesByRegionAsync(Guid companyId, int cityId);
    Task<AffiliateDto> CreateAffiliateAsync(Guid companyId, CreateAffiliateDto affiliateDto);
    Task<AffiliateDto> UpdateAffiliateAsync(Guid affiliateId, UpdateAffiliateDto affiliateDto);
    Task<bool> DeleteAffiliateAsync(Guid affiliateId);


    Task AddManager(Guid affiliateId, OperationAffiliateUserDto partnerDto);
    Task RemoveManager(Guid affiliateId, OperationAffiliateUserDto partnerDto);
    
    Task AddEmployee(Guid affiliateId, OperationAffiliateUserDto partnerDto);
    Task RemoveEmployee(Guid affiliateId, OperationAffiliateUserDto partnerDto);

    Task<List<CategoryDto>> GetAffiliateCategories(Guid affiliateId);
    Task AddCategory(Guid affiliateId, int categoryId);
    Task RemoveCategory(int categoryId);

    Task<List<ProductDto>> GetAffiliateProducts(Guid affiliateId);
    Task AddProduct(Guid affiliateId, int productId);
    Task RemoveProduct(int productId);
    Task ChangeProductAvailability(int productId, bool isAvailable);
}
