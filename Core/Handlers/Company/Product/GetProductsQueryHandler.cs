using Core.Dtos;
using Core.Dtos.Company.Category;
using Core.Dtos.Company.Product;
using Core.Interfaces;
using Core.Queries.Company.Category;
using Core.Queries.Company.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{
    private readonly ICompanyProductService _companyProductService;

    public GetProductsQueryHandler(ICompanyProductService companyProductService)
    {
        _companyProductService = companyProductService;
    }
    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _companyProductService.GetProductsAsync(request.CompanyId, request.CategoryId);
            return Result<List<ProductDto>>.Success(res);
        }
        catch (Exception ex)
        {
            return Result<List<ProductDto>>.Failure(new ErrorMessage("GetProductsError", ex.Message));
        }

    }
}