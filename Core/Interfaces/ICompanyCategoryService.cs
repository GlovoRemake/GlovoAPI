using Core.Dtos.Company;
using Core.Dtos.Company.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces;

public interface ICompanyCategoryService
{
    Task<List<CategoryDto>> GetAllCompanyCategoriesAsync(GetAllCategoriesDto query);
}
