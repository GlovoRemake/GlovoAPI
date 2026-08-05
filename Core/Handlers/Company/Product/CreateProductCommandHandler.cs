using Core.Commands.Company.Category;
using Core.Commands.Company.Product;
using Core.Dtos;
using Core.Dtos.Company.Product;
using Core.Dtos.Exceptions.Company.Category;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    private readonly ICompanyProductService _companyProductService;

    public CreateProductCommandHandler(ICompanyProductService companyProductService)
    {
        _companyProductService = companyProductService;
    }
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _companyProductService.CreateProductAsync(request.CompanyId, request.Dto);
            return Result<ProductDto>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure(new ErrorMessage("CreateProductError", ex.Message));
        }
    }
}
