using Core.Commands.Company.Category;
using Core.Commands.Company.Product.Additional;
using Core.Dtos;
using Core.Dtos.Company.Product.AdditionalGroup;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product.Additional;

public sealed class ReorderAdditionalCommandHandler : IRequestHandler<ReorderAdditionalCommand, Result>
{
    private readonly ICompanyProductAdditionalService _companyProductAdditionalService;

    public ReorderAdditionalCommandHandler(ICompanyProductAdditionalService companyProductAdditionalService)
    {
        _companyProductAdditionalService = companyProductAdditionalService;
    }
    public async Task<Result> Handle(ReorderAdditionalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _companyProductAdditionalService.ReorderAdditionalGroup(request.productId, request.dto);
            return Result.Success();
        }
        catch (InvalidDataException ex)
        {
            return Result.Failure(new ErrorMessage("InvalidData", ex.Message));
        }
        catch (Exception ex)
        {
            return Result.Failure(new ErrorMessage("ServerError", ex.Message));
        }
    }
}