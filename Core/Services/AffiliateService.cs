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
using Core.Dtos.Company.Category;
using Core.Dtos.Company.Product;
using Core.Dtos.Exceptions.Company.Product;
using Domain.Entities.Company.Product;
using Domain.Entities.Company.ProductCategory;

namespace Core.Services;

public class AffiliateService(
    ISoftDeleteRepository<Company, Guid> _companyRepo,
    IRepository<CompanyAffiliate, Guid> _affiliateRepo,
    IRepository<CompanyAffiliateLocation, int> _locationRepo,
    ISoftDeleteRepository<PartnerUser, Guid> _partnerRepo,
    ISoftDeleteRepository<Region, int> _regionRepo,
    ISoftDeleteRepository<Employee, int> _employeeRepo,
    ISoftDeleteRepository<PartnerRole, int> _partnerRoleRepo,
    ISoftDeleteRepository<CompanyProduct, int> _productRepo,
    ISoftDeleteRepository<CompanyProductCategory, int> _productCategoryRepo,
    ISoftDeleteRepository<CompanyAffiliateProduct, int> _affiliateProductRepo,
    ISoftDeleteRepository<CompanyAffiliatesProductsCategory, int> _affiliatesProductsCategoryRepo,
    IMapper _mapper
) : IAffiliateService
{

    public async Task<PagedAffiliatesDto> GetAllAffiliatesAsync(Guid companyId, int pageNumber, int pageSize)
    {
        var (requests, totalCount) = await _affiliateRepo.ListPagedAsync<AffiliateDto>(
            pageNumber,
            pageSize,
            predicate: x => x.CompanyId == companyId && !x.Company.IsDeleted
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

        var partner = await _partnerRepo.Query()
            .FirstOrDefaultAsync(x => x.Email == partnerDto.PartnerEmail && !x.IsDeleted);
        if (partner == null)
            throw new PartnerNotFound();

        var isAlready = await _employeeRepo.Query().AnyAsync(x =>
            x.CompanyAffiliateId == affiliateId && x.PartnerUserId == partner.Id && !x.IsDeleted);
        if (isAlready)
            throw new PartnerEmailAlreadyRegistered();

        await _employeeRepo.AddAsync(new Employee
        {
            PartnerUserId = partner.Id,
            CompanyAffiliateId = affiliateId,
            RoleId = await _partnerRoleRepo.Query().Where(x => x.Name == "Manager").Select(x => x.Id)
                .FirstOrDefaultAsync()
        });
    }

    public async Task RemoveManager(Guid affiliateId, OperationAffiliateUserDto partnerDto)
    {
        var affiliate = await _affiliateRepo.Query().AnyAsync(x => x.Id == affiliateId);
        if (!affiliate)
            throw new AffiliateNotFoundException();

        var partner = await _partnerRepo.Query()
            .FirstOrDefaultAsync(x => x.Email == partnerDto.PartnerEmail && !x.IsDeleted);
        if (partner == null)
            throw new PartnerNotFound();

        var employee = await _employeeRepo.Query()
            .Where(x => x.CompanyAffiliateId == affiliateId && x.PartnerUserId == partner.Id &&
                        x.Role.Name != "CompanyOwner" && !x.IsDeleted)
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync();
        if (employee == null)
            throw new PartnerNotFound();

        await _employeeRepo.DeleteAsync(employee ?? -1);
    }


    public async Task AddEmployee(Guid affiliateId, OperationAffiliateUserDto partnerDto)
    {
        var affiliate = await _affiliateRepo.Query().AnyAsync(x => x.Id == affiliateId);
        if (!affiliate)
            throw new AffiliateNotFoundException();

        var partner = await _partnerRepo.Query()
            .FirstOrDefaultAsync(x => x.Email == partnerDto.PartnerEmail && !x.IsDeleted);
        if (partner == null)
            throw new PartnerNotFound();

        var isAlready = await _employeeRepo.Query().AnyAsync(x =>
            x.CompanyAffiliateId == affiliateId && x.PartnerUserId == partner.Id && !x.IsDeleted);
        if (isAlready)
            throw new PartnerEmailAlreadyRegistered();

        await _employeeRepo.AddAsync(new Employee
        {
            PartnerUserId = partner.Id,
            CompanyAffiliateId = affiliateId,
            RoleId = await _partnerRoleRepo.Query().Where(x => x.Name == "Employee").Select(x => x.Id)
                .FirstOrDefaultAsync()
        });
    }

    public async Task RemoveEmployee(Guid affiliateId, OperationAffiliateUserDto partnerDto)
    {
        var affiliate = await _affiliateRepo.Query().AnyAsync(x => x.Id == affiliateId);
        if (!affiliate)
            throw new AffiliateNotFoundException();

        var partner = await _partnerRepo.Query()
            .FirstOrDefaultAsync(x => x.Email == partnerDto.PartnerEmail && !x.IsDeleted);
        if (partner == null)
            throw new PartnerNotFound();

        var employee = await _employeeRepo.Query()
            .Where(x => x.CompanyAffiliateId == affiliateId && x.PartnerUserId == partner.Id &&
                        x.Role.Name != "Manager" && x.Role.Name != "CompanyOwner" && !x.IsDeleted)
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync();
        if (employee == null)
            throw new PartnerNotFound();

        await _employeeRepo.DeleteAsync(employee ?? -1);
    }


    public async Task<List<CategoryDto>> GetAffiliateCategories(Guid affiliateId)
    {
        var categories = await _productCategoryRepo.Query()
            .Where(x => x.Affiliates.Any(a => a.CompanyAffiliateId == affiliateId && !a.IsDeleted) && !x.IsDeleted)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return categories;
    }
    
    public async Task AddCategory(Guid affiliateId, int categoryId)
    {
        var categoryCompany = await _productCategoryRepo.Query()
            .Where(x => x.Id == categoryId && !x.IsDeleted)
            .Select(x => (Guid?)x.CompanyId).FirstOrDefaultAsync();
        if (categoryCompany == null)
            throw new ProductNotFoundException();
        
        var affiliate = await _affiliateRepo.Query().AnyAsync(x => x.Id == affiliateId && x.CompanyId == categoryCompany);
        if (!affiliate)
            throw new AffiliateNotFoundException();

        await _affiliatesProductsCategoryRepo.AddAsync(new CompanyAffiliatesProductsCategory
        {
            CategoryId = categoryId,
            CompanyAffiliateId = affiliateId,
        });
    }
    
    public async Task RemoveCategory(Guid affiliateId, int categoryId)
    {
        var affiliateCategory = await _affiliatesProductsCategoryRepo.Query()
            .FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.CompanyAffiliateId == affiliateId && !x.IsDeleted);
        if (affiliateCategory == null)
            throw new ProductNotFoundException();

        affiliateCategory.IsDeleted = true;
        await _affiliatesProductsCategoryRepo.UpdateAsync(affiliateCategory);
    }
    
    
    public async Task<List<ProductDto>> GetAffiliateProducts(Guid affiliateId)
    {
        var products = await _productRepo.Query()
            .Where(x => x.Affiliates.Any(a => a.CompanyAffiliateId == affiliateId && !a.IsDeleted) && !x.IsDeleted)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return products;
    }
    
    public async Task AddProduct(Guid affiliateId, int productId)
    {
        var productCompany = await _productRepo.Query()
            .Where(x => x.Id == productId && !x.IsDeleted)
            .Select(x => (Guid?)x.CompanyId).FirstOrDefaultAsync();
        if (productCompany == null)
            throw new ProductNotFoundException();
        
        var affiliate = await _affiliateRepo.Query().AnyAsync(x => x.Id == affiliateId && x.CompanyId == productCompany);
        if (!affiliate)
            throw new AffiliateNotFoundException();

        await _affiliateProductRepo.AddAsync(new CompanyAffiliateProduct
        {
            ProdcutId = productId,
            CompanyAffiliateId = affiliateId,
            IsAvailable = true
        });
    }
    
    public async Task RemoveProduct(Guid affiliateId, int productId)
    {
        var affiliateProduct = await _affiliateProductRepo.Query()
            .FirstOrDefaultAsync(x => x.ProdcutId == productId && x.CompanyAffiliateId == affiliateId && !x.IsDeleted);
        if (affiliateProduct == null)
            throw new ProductNotFoundException();
        
        affiliateProduct.IsAvailable = false;
        affiliateProduct.IsDeleted = true;
        await _affiliateProductRepo.UpdateAsync(affiliateProduct);
    }

    public async Task ChangeProductAvailability(Guid affiliateId, int productId, bool isAvailable)
    {
        var affiliateProduct = await _affiliateProductRepo.Query()
            .FirstOrDefaultAsync(x => x.ProdcutId == productId && x.CompanyAffiliateId == affiliateId && !x.IsDeleted);
        if (affiliateProduct == null)
            throw new ProductNotFoundException();
        
        affiliateProduct.IsAvailable = isAvailable;
        await _affiliateProductRepo.UpdateAsync(affiliateProduct);
    }
}
