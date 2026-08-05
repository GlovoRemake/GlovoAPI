using Core.Dtos;
using Core.Dtos.Company;
using MediatR;

namespace Core.Queries.Company;

public record GetCompanyQuery(Guid CompanyId)
    : IRequest<Result<CompanyDto>>;
