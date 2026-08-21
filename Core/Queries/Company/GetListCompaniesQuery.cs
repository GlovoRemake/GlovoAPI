using Core.Dtos;
using Core.Dtos.Company;
using MediatR;

public record GetListCompaniesQuery(Guid partnerId)
    : IRequest<Result<List<CompanyDto>>>;