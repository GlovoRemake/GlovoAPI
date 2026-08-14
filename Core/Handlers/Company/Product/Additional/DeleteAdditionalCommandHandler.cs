using Core.Commands.Company.Product.Additional;
using Core.Dtos;
using Core.Dtos.Company.Product.AdditionalGroup;
using Core.Interfaces;
using Core.Queries.Company.Product.Additional;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product.Additional;

public sealed class DeleteAdditionalCommandHandler : IRequestHandler<DeleteAdditionalCommand, Result<bool>>
{
    private readonly ICompanyProductAdditionalService _companyProductAdditionalService;

    public DeleteAdditionalCommandHandler(ICompanyProductAdditionalService companyProductAdditionalService)
    {
        _companyProductAdditionalService = companyProductAdditionalService;
    }
    public async Task<Result<bool>> Handle(DeleteAdditionalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _companyProductAdditionalService.DeleteAdditionalGroup(request.additionalGroupId);
            return Result<bool>.Success(res);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new ErrorMessage("ServerError", ex.Message));
        }
    }
}