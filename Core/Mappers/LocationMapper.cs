using AutoMapper;
using Core.Dtos.Company.Affiliate.Location;
using Domain.Entities.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class LocationMapper : Profile
{
    public LocationMapper()
    {
        CreateMap<CreateAffiliateLocationDto, CompanyAffiliateLocation>();
        CreateMap<CompanyAffiliateLocation, LocationDto>()
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region.Name));
    }
}
