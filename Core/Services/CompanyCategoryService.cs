using AutoMapper;
using AutoMapper.Internal.Mappers;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Company;
using Core.Dtos.Company.Category;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Company.Category;
using Core.Interfaces;
using Domain.Data;
using Domain.Entities.Company;
using Domain.Entities.Company.ProductCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.Services;

public class CompanyCategoryService(
        IMapper _mapper,
        ISoftDeleteRepository<CompanyProductCategory, int> _companyCategoryRepo
    ) : ICompanyCategoryService
{
    public async Task<List<CategoryDto>> GetAllCompanyCategoriesAsync(GetAllCategoriesDto query)
    {
        return await _companyCategoryRepo.Query().Where(x => x.CompanyId == query.CompanyId && !x.IsDeleted)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
    public async Task AddCompanyCategoriesAsync(AddCategoryDto dto)
    {
        var existingCategory = await _companyCategoryRepo.Query()
            .FirstOrDefaultAsync(x => x.CompanyId == dto.CompanyId && x.Name == dto.Name && !x.IsDeleted);
        if (existingCategory != null)
        {
            throw new CategoryAlreadyExistsException("Категорія вже існує");
        }

        var lastOrder = await _companyCategoryRepo.Query()
            .Where(x => x.CompanyId == dto.CompanyId && !x.IsDeleted)
            .OrderByDescending(x => x.Order)
            .FirstOrDefaultAsync();

        var entity = _mapper.Map<CompanyProductCategory>(dto);
        if (lastOrder != null)
        {
            entity.Order = lastOrder.Order + 1;
        }

        await _companyCategoryRepo.AddAsync(entity);
    }

    public async Task DeleteCompanyCategoriesAsync(DeleteCategoryDto dto)
    {
        await _companyCategoryRepo.DeleteAsync(dto.IdCategory);
    }


    public async Task UpdateCompanyCategoriesAsync(UpdateCategoryDto dto)
    {
        var category = await _companyCategoryRepo.GetByIdAsync(dto.IdCategory);
        if (category == null)
        {
            throw new CategoryNotFoundException("Категорія не знайдена");
        }

        _mapper.Map(dto, category);
        await _companyCategoryRepo.UpdateAsync(category);
    }

    public async Task ReorderCompanyCategoriesAsync(ReorderCategoryDto dto)
    {
        var categories = await _companyCategoryRepo.Query()
            .Where(x => x.CompanyId == dto.CompanyId && dto.CategoryIds.Contains(x.Id) && !x.IsDeleted)
            .ToListAsync();

        if (categories.Count() != dto.CategoryIds.Count())
        {
            throw new InvalidDataException("Неправильний перелік категорій, ви мабуть хотіли зманіпулювати системою :(");
        }

        short order = 1;
        foreach (var categoryId in dto.CategoryIds)
        {
            categories.FirstOrDefault(x => x.Id == categoryId).Order = order;
            order++;
        }
        foreach (var category in categories)
        {
            await _companyCategoryRepo.UpdateAsync(category);
        }
        await _companyCategoryRepo.SaveChangesAsync();
    }
}
