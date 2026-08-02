using Core.Dtos.Company.Affiliate;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class AffiliateService(

    ) : IAffiliateService
{
    
    public Task<AffiliateDto[]> GetAllAffiliatesAsync(Guid companyId, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
    public Task<AffiliateDto[]> GetAllAffiliatesByRegionAsync(Guid companyId, int cityId)
    {
        throw new NotImplementedException();
    }

    public Task<AffiliateDto> CreateAffiliateAsync(Guid comapanyId, CreateAffiliateDto affiliateDto)
    {
        throw new NotImplementedException();
    }

    public Task<AffiliateDto> UpdateAffiliateAsync(Guid affiliateId, UpdateAffiliateDto affiliateDto)
    {
        throw new NotImplementedException();
    }
    public Task<bool> DeleteAffiliateAsync(Guid affiliateId)
    {
        throw new NotImplementedException();
    }
}
