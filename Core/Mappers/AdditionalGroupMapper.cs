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
        CreateMap<UpdateAdditionalGroupDto, AdditionalGroup>()
            .ForMember(x => x.Additionals, opt => opt.Ignore());
        CreateMap<AdditionalGroup, AdditionalGroupDto>()
            .ForMember(
                x => x.Additionals,
                opt => opt.MapFrom(x => x.Additionals.Where(a => !a.IsDeleted))
            );
    }
}
