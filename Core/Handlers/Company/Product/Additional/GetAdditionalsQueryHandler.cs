using Core.Commands.Company.Product.Additional;
using Core.Dtos;
using Core.Dtos.Company.Product;
using Core.Dtos.Company.Product.AdditionalGroup;
using Core.Dtos.Exceptions.Company.Product;
using Core.Interfaces;
using Core.Queries.Company.Product;
using Core.Queries.Company.Product.Additional;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product.Additional;

public sealed class GetAdditionalsQueryHandler : IRequestHandler<GetAdditionalsQuery, Result<List<AdditionalGroupDto>>>
{
    private readonly ICompanyProductAdditionalService _companyProductAdditionalService;

    public GetAdditionalsQueryHandler(ICompanyProductAdditionalService companyProductAdditionalService)
    {
        _companyProductAdditionalService = companyProductAdditionalService;
    }
    public async Task<Result<List<AdditionalGroupDto>>> Handle(GetAdditionalsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var additionals = await _companyProductAdditionalService.GetAdditionalsGroup(request.productId);
            return Result<List<AdditionalGroupDto>>.Success(additionals);
        }
        catch (Exception ex)
        {
            return Result<List<AdditionalGroupDto>>.Failure(new ErrorMessage("ServerError", ex.Message));
        }
    }
}