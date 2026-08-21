using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Core.Queries.Company;
using MediatR;

namespace Core.Handlers.Company;

public sealed class GetListCompaniesQueryHandler : IRequestHandler<GetListCompaniesQuery, Result<List<CompanyDto>>>
{
    private readonly ICompanyService _companyService;

    public GetListCompaniesQueryHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task<Result<List<CompanyDto>>> Handle(GetListCompaniesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Result<List<CompanyDto>>.Success(await _companyService.GetListCompanyAsync(request.partnerId));
        }
        catch (Exception ex)
        {
            return Result<List<CompanyDto>>.Failure(new ErrorMessage("InternalError", ex.Message));
        }
    }
}
