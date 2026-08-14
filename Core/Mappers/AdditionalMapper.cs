using AutoMapper;
using Core.Dtos.Company.Product.Additional;
using Domain.Entities.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class AdditionalMapper : Profile
{
    public AdditionalMapper()
    {
        CreateMap<CreateAdditionalDto, Additional>();
        CreateMap<Additional, AdditionalDto>();
    }
}
