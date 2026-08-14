using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Product.Additional;

public class UpdateAdditionalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}
