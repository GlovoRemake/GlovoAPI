using AutoMapper;
using Core.Dtos.Company.Category;
using Domain.Entities.Company.ProductCategory;

namespace Core.Mappers;

public class CompanyCategoryMapper : Profile
{
    public CompanyCategoryMapper()
    {
        CreateMap<CompanyProductCategory, CategoryDto>();
    }
}
