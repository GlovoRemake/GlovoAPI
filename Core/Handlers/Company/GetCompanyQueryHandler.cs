using Core.Dtos;
using Core.Dtos.Company;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using Core.Queries.Company;
using MediatR;

namespace Core.Handlers.Company;

public sealed class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, Result<CompanyDto>>
{
    private readonly ICompanyService _companyService;

    public GetCompanyQueryHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task<Result<CompanyDto>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var company = await _companyService.GetCompanyAsync(request.CompanyId);
            return company == null ? throw new CompanyNotFoundException("Компанія не знайдена") : Result<CompanyDto>.Success(company);
        }
        catch (CompanyNotFoundException ex)
        {
            return Result<CompanyDto>.Failure(new ErrorMessage("CompanyNotFound", ex.Message));
        }
        catch (Exception ex)
        {
            return Result<CompanyDto>.Failure(new ErrorMessage("InternalError", ex.Message));
        }
    }
}
