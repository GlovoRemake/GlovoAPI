using AutoMapper;
using AutoMapper.Internal.Mappers;
using Core.Dtos.Company;
using Core.Dtos.Company.Category;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Domain.Entities.Company;
using Domain.Entities.Company.ProductCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class CompanyCategoryService(
        IMapper _mapper
    ) : ICompanyCategoryService
{
    public Task<List<CategoryDto>> GetAllCompanyCategoriesAsync(GetAllCategoriesDto query)
    {
        throw new NotImplementedException();
    }
}
