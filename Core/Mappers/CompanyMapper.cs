using AutoMapper;
using Core.Dtos.Company;
using Domain.Entities.Company;

namespace Core.Mappers;

public class CompanyMapper : Profile
{
    public CompanyMapper()
    {
        CreateMap<AddRequestCompanyDto, RequestCompany>();

        CreateMap<RequestCompany, RequestCompanyDto>();

        CreateMap<RequestCompany, Company>()
            .ForMember(x => x.OwnerId, opt => opt.MapFrom(x => x.PartnerId))
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<Company, CompanyDto>();
    }   
}

