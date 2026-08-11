using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Company;
using Core.Dtos.Company.Affiliate;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Company.Affiliate;
using Core.Dtos.Exceptions.Partner;
using Core.Dtos.Exceptions.Region;
using Core.Interfaces;
using Domain.Entities;
using Domain.Entities.Company;
using Domain.Entities.Company.Affiliate;
using Domain.Entities.Company.Partner;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Core.Services;

public class AffiliateService(
        ISoftDeleteRepository<Company, Guid> _companyRepo,
        IRepository<CompanyAffiliate, Guid> _affiliateRepo,
        IRepository<CompanyAffiliateLocation, int> _locationRepo,
        ISoftDeleteRepository<PartnerUser, Guid> _partnerRepo,
        ISoftDeleteRepository<Region, int> _regionRepo,
        ISoftDeleteRepository<Employee, int> _employeeRepo,
        ISoftDeleteRepository<PartnerRole, int> _partnerRoleRepo,
        IMapper _mapper
    ) : IAffiliateService
{
    
    public async Task<PagedAffiliatesDto> GetAllAffiliatesAsync(Guid companyId, int pageNumber, int pageSize)
    {
        var (requests, totalCount) = await _affiliateRepo.ListPagedAsync<AffiliateDto>(
            pageNumber,
            pageSize,
            predicate: x => true
        );

        return new PagedAffiliatesDto
        {
            Affiliates = requests.ToList(),
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
    public async Task<AffiliateDto[]> GetAllAffiliatesByRegionAsync(Guid companyId, int cityId)
    {
        return await _affiliateRepo.Query()
            .Where(x => x.CompanyId == companyId && x.Location.RegionId == cityId)
            .ProjectTo<AffiliateDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync();
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

    public async Task AddManager(Guid affiliateId, OperationAffiliateUserDto partnerDto)
    {
        var affiliate = await _affiliateRepo.Query().AnyAsync(x => x.Id == affiliateId);
        if (!affiliate)
            throw new AffiliateNotFoundException();

        var partner = await _partnerRepo.Query().FirstOrDefaultAsync(x => x.Email == partnerDto.PartnerEmail && !x.IsDeleted);
        if (partner == null)
            throw new PartnerNotFound();

        var isAlready = await _employeeRepo.Query().AnyAsync(x => x.CompanyAffiliateId == affiliateId && x.PartnerUserId == partner.Id && !x.IsDeleted);
        if (isAlready)
            throw new PartnerEmailAlreadyRegistered();

        await _employeeRepo.AddAsync(new Employee
        {
            PartnerUserId = partner.Id,
            CompanyAffiliateId = affiliateId,
            RoleId = await _partnerRoleRepo.Query().Where(x => x.Name == "Manager").Select(x => x.Id).FirstOrDefaultAsync()
        });
    }

    public async Task RemoveManager(Guid affiliateId, OperationAffiliateUserDto partnerDto)
    {
        var affiliate = await _affiliateRepo.Query().AnyAsync(x => x.Id == affiliateId);
        if (!affiliate)
            throw new AffiliateNotFoundException();

        var partner = await _partnerRepo.Query().FirstOrDefaultAsync(x => x.Email == partnerDto.PartnerEmail && !x.IsDeleted);
        if (partner == null)
            throw new PartnerNotFound();

        var employee = await _employeeRepo.Query()
            .Where(x => x.CompanyAffiliateId == affiliateId && x.PartnerUserId == partner.Id && !x.IsDeleted)
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync();
        if (employee == null)
            throw new PartnerNotFound();

        await _employeeRepo.DeleteAsync(employee ?? -1);
    }
}
