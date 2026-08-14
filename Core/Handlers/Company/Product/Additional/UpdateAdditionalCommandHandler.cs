using Core.Commands.Company.Product.Additional;
using Core.Dtos;
using Core.Dtos.Company.Product.AdditionalGroup;
using Core.Dtos.Exceptions.Company.Product;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product.Additional;

public sealed class UpdateAdditionalCommandHandler : IRequestHandler<UpdateAdditionalCommand, Result<AdditionalGroupDto>>
{
    private readonly ICompanyProductAdditionalService _companyProductAdditionalService;

    public UpdateAdditionalCommandHandler(ICompanyProductAdditionalService companyProductAdditionalService)
    {
        _companyProductAdditionalService = companyProductAdditionalService;
    }
    public async Task<Result<AdditionalGroupDto>> Handle(UpdateAdditionalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _companyProductAdditionalService.UpdateAdditionalGroup(request.additionalGroupId, request.dto);
            return Result<AdditionalGroupDto>.Success(product);
        }
        catch (ProductNotFoundException)
        {
            return Result<AdditionalGroupDto>.Failure(new ErrorMessage("AdditionalNotFound", "Додаток не знайдено"));
        }
        catch (Exception ex)
        {
            return Result<AdditionalGroupDto>.Failure(new ErrorMessage("CreateProductError", ex.Message));
        }
    }
}