using Core.Commands.Company;
using Core.Dtos;
using Core.Dtos.Exceptions.Company;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers.Company;

public sealed class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Result>
{
    private readonly ICompanyService _companyService;

    public UpdateCompanyCommandHandler(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    public async Task<Result> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyService.UpdateCompanyAsync(request.dto);
            return Result.Success();
        }
        catch (CompanyNotFoundException ex)
        {
            return Result.Failure(new ErrorMessage("CompanyNotFound", ex.Message));
        }
        catch (Exception)
        {
            return Result.Failure(new ErrorMessage("UpdateCompanyError", "Виникла помилка при оновленні компанії."));
        }
    }
}
