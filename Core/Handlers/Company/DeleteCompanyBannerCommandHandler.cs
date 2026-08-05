using Core.Commands.Company;
using Core.Dtos;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Company;

public sealed class DeleteCompanyBannerCommandHandler : IRequestHandler<DeleteCompanyBannerCommand, Result>
{
    private readonly ICompanyService _companyService;

    public DeleteCompanyBannerCommandHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task<Result> Handle(DeleteCompanyBannerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyService.DeleteCompanyBannerAsync(request.CompanyId);
            return Result.Success();
        }
        catch (CompanyNotFoundException ex)
        {
            return Result.Failure(new ErrorMessage("CompanyNotFound", ex.Message));
        }
        catch (Exception)
        {
            return Result.Failure(new ErrorMessage("DeleteCompanyBannerError", "Виникла помилка при видаленні баннера компанії."));
        }
    }
}
