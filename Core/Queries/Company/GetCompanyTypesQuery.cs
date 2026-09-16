using Core.Dtos;
using Core.Dtos.Company;
using MediatR;

namespace Core.Queries.Company;

public record GetCompanyTypesQuery()
    : IRequest<Result<List<CompanyTypeDto>>> { };