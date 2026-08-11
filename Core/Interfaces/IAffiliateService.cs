using Core.Dtos.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;

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
}
