using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Company.Product.AdditionalGroup;
using Core.Dtos.Exceptions.Company.Product;
using Core.Interfaces;
using Domain.Entities.Company.Product;
using Domain.Entities.Company.Product.Additional;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class CompanyProductAdditionalService(
        ISoftDeleteRepository<CompanyProduct, int> _productRepo,
        ISoftDeleteRepository<AdditionalGroup, int> _additionalRepo,
        IMapper _mapper
    ) : ICompanyProductAdditionalService
{
    public async Task<List<AdditionalGroupDto>> GetAdditionalsGroup(int productId)
    {
        return await _additionalRepo.Query()
            .Where(x => !x.IsDeleted && x.ProductId == productId)
            .ProjectTo<AdditionalGroupDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<AdditionalGroupDto> CreateAdditionalGroup(int productId, CreateAdditionalGroupDto dto)
    {
        var product = await _productRepo.Query().AnyAsync(x => x.Id == productId);
        if (!product)
            throw new ProductNotFoundException();

        var entity = _mapper.Map<AdditionalGroup>(dto);

        int initialOrder = 1;

        foreach (var item in entity.Additionals)
        {
            item.Order = initialOrder;
            initialOrder++;
        }

        var maxOrder = await _additionalRepo.Query()
            .Where(x => x.ProductId == productId && !x.IsDeleted)
            .MaxAsync(x => (int?)x.Order) ?? 0;

        entity.ProductId = productId;
        entity.Order = maxOrder + 1;
        await _additionalRepo.AddAsync(entity);

        return _mapper.Map<AdditionalGroupDto>(entity);
    }

    public Task<AdditionalGroupDto> UpdateAdditionalGroup(int additionalGroupId, UpdateAdditionalGroupDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAdditionalGroup(int additionalGroupId)
    {
        throw new NotImplementedException();
    }

    public Task<AdditionalGroupDto> ReorderAdditionalGroup(int additionalGroupId, ReorderAdditionalGroupDto dto)
    {
        throw new NotImplementedException();
    }
}
