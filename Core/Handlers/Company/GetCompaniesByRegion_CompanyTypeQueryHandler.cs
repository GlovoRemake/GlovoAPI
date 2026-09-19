using Core.Dtos;
using Core.Dtos.Company;
using Core.Interfaces;
using Core.Queries.Company;
using MediatR;

namespace Core.Handlers.Company;

public class GetCompaniesByRegion_CompanyTypeQueryHandler : IRequestHandler<GetCompaniesByRegion_CompanyTypeQuery, Result<List<CompanyDto?>>>
{
    private readonly ICompanyService _companyService;

    public GetCompaniesByRegion_CompanyTypeQueryHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task<Result<List<CompanyDto?>>> Handle(GetCompaniesByRegion_CompanyTypeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var companies = await _companyService.GetCompaniesByRegion_CompanyType(request.RegionId, request.CompanyTypeIds);
            return Result<List<CompanyDto?>>.Success(companies);
        }
        catch (Exception ex)
        {
            return Result<List<CompanyDto?>>.Failure(new ErrorMessage("UNDEFINED ERROR", ex.Message));
        }
    }
}
