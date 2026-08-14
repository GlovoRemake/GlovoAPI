using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Product.Additional;

public class AdditionalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Order { get; set; }
}
