using AutoMapper;
using Core.Commands.Company.Category;
using Core.Dtos.Company.Product;
using Domain.Entities.Company.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class CompanyProductMapper : Profile
{
    public CompanyProductMapper()
    {
        CreateMap<CreateProductDto, CompanyProduct>()
            .ForMember(x => x.Order, opt => opt.Ignore())
            .ForMember(x => x.ImagePath, opt => opt.Ignore())
            .ForMember(x => x.CompanyId, opt => opt.Ignore());

        CreateMap<UpdateProductDto, CompanyProduct>()
            .ForMember(x => x.Order, opt => opt.Ignore())
            .ForMember(x => x.ImagePath, opt => opt.Ignore());

        CreateMap<CompanyProduct, ProductDto>();
    }
}
