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
    }   
}

