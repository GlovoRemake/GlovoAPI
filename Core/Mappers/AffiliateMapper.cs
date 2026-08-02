using AutoMapper;
using Core.Dtos.Company.Affiliate;
using Domain.Entities.Company;
using Domain.Entities.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Mappers;

public class AffiliateMapper : Profile
{
    public AffiliateMapper()
    {
        CreateMap<CreateAffiliateDto, CompanyAffiliate>()
            .ForMember(x => x.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(x => x.WorkingHours, opt => opt.Ignore());

        CreateMap<UpdateAffiliateDto, CompanyAffiliate>()
            .ForMember(x => x.Location, opt => opt.Ignore())
            .ForMember(x => x.WorkingHours, opt => opt.Ignore());

        CreateMap<CompanyAffiliate, AffiliateDto>();
    }
}
