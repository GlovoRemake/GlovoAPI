using Core.Commands.Company;
using Core.Dtos;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Company;

public sealed class DeleteCompanyIconCommandHandler : IRequestHandler<DeleteCompanyIconCommand, Result>
{
    private readonly ICompanyService _companyService;

    public DeleteCompanyIconCommandHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task<Result> Handle(DeleteCompanyIconCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyService.DeleteCompanyIconAsync(request.CompanyId);
            return Result.Success();
        }
        catch (CompanyNotFoundException ex)
        {
            return Result.Failure(new ErrorMessage("CompanyNotFound", ex.Message));
        }
        catch (Exception)
        {
            return Result.Failure(new ErrorMessage("DeleteCompanyIconError", "Виникла помилка при видаленні іконки компанії."));
        }
    }
}
