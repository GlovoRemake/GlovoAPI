using Core.Commands.Company.Product;
using Core.Dtos;
using Core.Dtos.Company.Product;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
{
    private readonly ICompanyProductService _companyProductService;

    public UpdateProductCommandHandler(ICompanyProductService companyProductService)
    {
        _companyProductService = companyProductService;
    }
    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _companyProductService.UpdateProductAsync(request.ProductId, request.Dto);
            return Result<ProductDto>.Success(product);
        }
        catch (Exception ex)
        {
            return Result<ProductDto>.Failure(new ErrorMessage("UpdateProductError", ex.Message));
        }
    }
}