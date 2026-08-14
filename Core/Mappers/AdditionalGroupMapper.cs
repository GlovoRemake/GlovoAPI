using AutoMapper;
using Core.Dtos.Company.Product.AdditionalGroup;
using Domain.Entities.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class AdditionalGroupMapper : Profile
{
    public AdditionalGroupMapper()
    {
        CreateMap<CreateAdditionalGroupDto, AdditionalGroup>();
        CreateMap<AdditionalGroup, AdditionalGroupDto>();
    }
}
