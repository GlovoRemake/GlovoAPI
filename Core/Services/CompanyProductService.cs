using Core.Dtos.Company.Product;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services;

public class CompanyProductService : ICompanyProductService
{
    public Task<ProductDto> CreateProductAsync(Guid CompanyId, CreateProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductAsync(int ProductId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductDto>> GetProductsAsync(Guid CompanyId, int CategoryId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> UpdateProductAsync(int ProductId, UpdateProductDto productDto)
    {
        throw new NotImplementedException();
    }
}
