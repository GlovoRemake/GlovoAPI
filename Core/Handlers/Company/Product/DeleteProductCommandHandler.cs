using Core.Commands.Company.Product;
using Core.Dtos;
using Core.Dtos.Company.Product;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Handlers.Company.Product;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
{
    private readonly ICompanyProductService _companyProductService;

    public DeleteProductCommandHandler(ICompanyProductService companyProductService)
    {
        _companyProductService = companyProductService;
    }
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _companyProductService.DeleteProductAsync(request.ProductId);
            return Result<bool>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new ErrorMessage("DeleteProductError", ex.Message));
        }
    }
}