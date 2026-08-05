using Core.Dtos.Company.Product;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Interfaces;

public interface ICompanyProductService
{
    Task<List<ProductDto>> GetProductsAsync(Guid CompanyId, int CategoryId);
    Task<ProductDto> CreateProductAsync(Guid CompanyId, CreateProductDto productDto);
    Task<ProductDto> UpdateProductAsync(int ProductId, UpdateProductDto productDto);
    Task<bool> DeleteProductAsync(int ProductId);

}
