using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Product;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = default!;
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public double Weight { get; set; }
    public double Kcal { get; set; }
}
