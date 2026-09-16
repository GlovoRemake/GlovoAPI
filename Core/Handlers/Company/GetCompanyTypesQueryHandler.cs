using Core.Dtos;
using Core.Dtos.Company;
using Core.Interfaces;
using Core.Queries.Company;
using MediatR;

namespace Core.Handlers.Company;

public class GetCompanyTypesQueryHandler : IRequestHandler<GetCompanyTypesQuery, Result<List<CompanyTypeDto>>>
{
    private readonly ICompanyService _companyService;

    public GetCompanyTypesQueryHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task<Result<List<CompanyTypeDto>>> Handle(GetCompanyTypesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _companyService.GetAllCompanyTypes();
            return Result<List<CompanyTypeDto>>.Success(res);
        }
        catch (Exception ex)
        {
            return Result<List<CompanyTypeDto>>.Failure(new ErrorMessage("Error", $"An error occurred while retrieving company types: {ex.Message}"));
        }
    }
}
