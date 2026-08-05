using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Company.Product;
using Core.Dtos.Exceptions.Company;
using Core.Dtos.Exceptions.Company.Category;
using Core.Dtos.Exceptions.Company.Product;
using Core.Interfaces;
using Domain.Entities.Company;
using Domain.Entities.Company.Product;
using Domain.Entities.Company.ProductCategory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Core.Services;

public class CompanyProductService(
        ISoftDeleteRepository<Company, Guid> _companyRepo,
        ISoftDeleteRepository<CompanyProductCategory, int> _categoryRepo,
        ISoftDeleteRepository<CompanyProduct, int> _productRepo,
        IMapper _mapper,
        IImageService _imageService
    ) : ICompanyProductService
{
    public async Task<List<ProductDto>> GetProductsAsync(Guid CompanyId, int CategoryId)
    {
        var company = await _companyRepo.Query().AnyAsync(x => x.Id == CompanyId);
        if (!company)
            throw new CompanyNotFoundException("Компанія не знайдена");

        var category = await _categoryRepo.Query().FirstOrDefaultAsync(x => x.Id == CategoryId && x.CompanyId == CompanyId);
        if (category == null)
            throw new CategoryNotFoundException("Категорія не знайдена");

        return await _productRepo.Query()
            .Where(x => x.CompanyId == CompanyId && x.CategoryId == CategoryId && !x.IsDeleted)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ProductDto> CreateProductAsync(Guid CompanyId, CreateProductDto productDto)
    {
        if (productDto.Price <= 0)
            throw new ArgumentException("Ціна має бути > 0");

        var company = await _companyRepo.Query().AnyAsync(x => x.Id == CompanyId);
        if (!company)
            throw new CompanyNotFoundException("Компанія не знайдена");

        var category = await _categoryRepo.Query().FirstOrDefaultAsync(x => x.Id == productDto.CategoryId && x.CompanyId == CompanyId);
        if (category == null)
            throw new CategoryNotFoundException("Категорія не знайдена");

        var lastOrder = await _productRepo.Query()
            .Where(x => x.CompanyId == CompanyId && !x.IsDeleted)
            .OrderByDescending(x => x.Order)
            .FirstOrDefaultAsync();

        var product = _mapper.Map<CompanyProduct>(productDto);
        product.CompanyId = CompanyId;
        product.Order = lastOrder?.Order + 1 ?? 1;
        product.ImagePath = await _imageService.SaveImageAsync(productDto.Image);
        await _productRepo.AddAsync(product);

        product.Category = category;
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateProductAsync(int ProductId, UpdateProductDto productDto)
    {
        if (productDto.Price <= 0)
            throw new ArgumentException("Ціна має бути > 0");

        var productCompany = await _productRepo.Query()
            .Where(x => x.Id == ProductId && !x.IsDeleted)
            .Select(x => new
            {
                x.CompanyId,
                x.ImagePath
            })
            .FirstOrDefaultAsync();
        if (productCompany is null)
            throw new ProductNotFoundException("Продукт не знайдений");

        var category = await _categoryRepo.Query().FirstOrDefaultAsync(x => x.Id == productDto.CategoryId && x.CompanyId == productCompany.CompanyId);
        if (category == null)
            throw new CategoryNotFoundException("Категорія не знайдена");

        var product = _mapper.Map<CompanyProduct>(productDto);
        product.Id = ProductId;
        product.CompanyId = productCompany.CompanyId;
        product.CategoryId = category.Id;

        if (productDto.Image != null)
        {
            await _imageService.DeleteImageAsync(productCompany.ImagePath);
            product.ImagePath = await _imageService.SaveImageAsync(productDto.Image);
        } else
        {
            product.ImagePath = productCompany.ImagePath;
        }

        await _productRepo.UpdateAsync(product);

        product.Category = category;
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> DeleteProductAsync(int ProductId)
    {
        var product = await _productRepo.Query().FirstOrDefaultAsync(x => x.Id == ProductId && !x.IsDeleted);
        if (product == null)
            throw new ProductNotFoundException("Продукт не знайдений");

        await _productRepo.DeleteAsync(product.Id);
        return true;
    }
}   
