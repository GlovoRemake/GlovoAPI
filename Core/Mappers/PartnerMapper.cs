using AutoMapper;
using Core.Dtos.Partner;
using Domain.Entities.Company.Partner;

namespace Core.Mappers;

public class PartnerMapper : Profile
{
    public PartnerMapper()
    {
        CreateMap<PartnerRegisterDto, PartnerUser>()
            .ForMember(x => x.PasswordHash, opt => opt.Ignore());
    }
}