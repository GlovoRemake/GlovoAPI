using AutoMapper;
using AutoMapper.Internal.Mappers;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Company;
using Core.Dtos.Company.Category;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Domain.Data;
using Domain.Entities.Company;
using Domain.Entities.Company.ProductCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
}
