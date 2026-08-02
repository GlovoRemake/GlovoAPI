using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Company.Affiliate;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Company.Affiliate;
using Core.Dtos.Exceptions.Region;
using Core.Interfaces;
using Domain.Entities;
using Domain.Entities.Company;
using Domain.Entities.Company.Affiliate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class AffiliateService(
        ISoftDeleteRepository<Company, Guid> _companyRepo,
        IRepository<CompanyAffiliate, Guid> _affiliateRepo,
        IRepository<CompanyAffiliateLocation, int> _locationRepo,
        ISoftDeleteRepository<Region, int> _regionRepo,
        IMapper _mapper
    ) : IAffiliateService
{
    
    public async Task<AffiliateDto[]> GetAllAffiliatesAsync(Guid companyId, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
    public async Task<AffiliateDto[]> GetAllAffiliatesByRegionAsync(Guid companyId, int cityId)
    {
        throw new NotImplementedException();
    }

    public async Task<AffiliateDto> CreateAffiliateAsync(Guid comapanyId, CreateAffiliateDto affiliateDto)
    {
        var company = await _companyRepo.Query().FirstOrDefaultAsync(x => x.Id == comapanyId);
        if (company == null)
            throw new CompanyNotFoundException();

        var region = await _regionRepo.Query().FirstOrDefaultAsync(x => x.Id == affiliateDto.Location.RegionId);
        if (region == null)
            throw new RegionNotFoundException();

        var affiliate = _mapper.Map<CompanyAffiliate>(affiliateDto);
        affiliate.CompanyId = comapanyId;
        await _affiliateRepo.AddAsync(affiliate);

        var newAffiliate = await _affiliateRepo.Query()
            .ProjectTo<AffiliateDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == affiliate.Id);

        return newAffiliate;
    }
    public async Task<AffiliateDto> UpdateAffiliateAsync(Guid affiliateId, UpdateAffiliateDto affiliateDto)
    {
        var affiliate = await _affiliateRepo.Query().FirstOrDefaultAsync(x => x.Id == affiliateId);
        if (affiliate == null)
            throw new AffiliateNotFoundException();

        _mapper.Map(affiliateDto, affiliate);
        await _affiliateRepo.UpdateAsync(affiliate);

        var updatedAffiliate = await _affiliateRepo.Query()
            .ProjectTo<AffiliateDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == affiliate.Id);

        return updatedAffiliate;
    }
    public async Task<bool> DeleteAffiliateAsync(Guid affiliateId)
    {
        var affiliate = await _affiliateRepo.Query().FirstOrDefaultAsync(x => x.Id == affiliateId);
        if (affiliate == null)
            throw new AffiliateNotFoundException();

        await _affiliateRepo.DeleteAsync(affiliate.Id);
        await _locationRepo.DeleteAsync(affiliate.LocationId);
        return true;
    }
}
