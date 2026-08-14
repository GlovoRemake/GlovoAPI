using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Company.Category;
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

    public async Task<AdditionalGroupDto> UpdateAdditionalGroup(int additionalGroupId, UpdateAdditionalGroupDto dto)
    {
        var additional = await _additionalRepo.Query()
            .Include(x => x.Additionals)
            .Where(x => !x.IsDeleted && x.Id == additionalGroupId)
            .FirstOrDefaultAsync();

        if (additional == null)
            throw new ProductNotFoundException();

        _mapper.Map(dto, additional);

        var existingAdditionals = additional.Additionals.ToList();

        var incomingIds = dto.Additionals
            .Where(x => x.Id.HasValue)
            .Select(x => x.Id!.Value)
            .ToHashSet();

        foreach (var existing in existingAdditionals)
        {
            if (!incomingIds.Contains(existing.Id))
            {
                existing.IsDeleted = true;
            }
        }

        for (int i = 0; i < dto.Additionals.Count; i++)
        {
            var additionDto = dto.Additionals[i];

            if (additionDto.Id.HasValue)
            {
                var existing = existingAdditionals
                    .FirstOrDefault(x => x.Id == additionDto.Id.Value);

                if (existing == null)
                    throw new KeyNotFoundException(
                        $"Additional with id {additionDto.Id.Value} not found.");

                _mapper.Map(additionDto, existing);

                existing.Order = i + 1;
            }
            else
            {
                var newAdditional = _mapper.Map<Additional>(additionDto);

                newAdditional.Order = i + 1;
                newAdditional.AdditionalGroupId = additional.Id;

                additional.Additionals.Add(newAdditional);
            }
        }

        await _additionalRepo.SaveChangesAsync();

        return _mapper.Map<AdditionalGroupDto>(additional);
    }

    public async Task<bool> DeleteAdditionalGroup(int additionalGroupId)
    {
        var additional = await _additionalRepo.Query()
            .Include(x => x.Additionals)
            .Where(x => !x.IsDeleted && x.Id == additionalGroupId)
            .FirstOrDefaultAsync();

        foreach (var item in additional.Additionals)
        {
            item.IsDeleted = true;
        }

        additional.IsDeleted = true;
        await _additionalRepo.SaveChangesAsync();

        return true;
    }

    public async Task ReorderAdditionalGroup(int productId, ReorderAdditionalGroupDto dto)
    {
        var additionals = await _additionalRepo.Query()
            .Where(x => x.ProductId == productId && dto.Ids.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync();

        if (additionals.Count() != dto.Ids.Count())
        {
            throw new InvalidDataException("Неправильний перелік додатків, ви мабуть хотіли зманіпулювати системою :(");
        }

        short order = 1;
        foreach (var categoryId in dto.Ids)
        {
            additionals.FirstOrDefault(x => x.Id == categoryId).Order = order;
            order++;
        }
        foreach (var additional in additionals)
        {
            await _additionalRepo.UpdateAsync(additional);
        }
        await _additionalRepo.SaveChangesAsync();
    }
}
