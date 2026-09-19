using Core.Dtos;
using Core.Dtos.Company;
using MediatR;

namespace Core.Queries.Company;

public record GetCompaniesByRegion_CompanyTypeQuery(int RegionId, int[]? CompanyTypeIds)
    : IRequest<Result<List<CompanyDto?>>> { }