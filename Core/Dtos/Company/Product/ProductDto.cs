using Core.Dtos.Company.Category;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public int Order { get; set; }
    public double Price { get; set; }
    public CategoryDto Category { get; set; } = default!;
    public double? Weight { get; set; }
    public WeightType? WeightType { get; set; }
    public double? Kcal { get; set; }
}
