using Core.Dtos.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Product.AdditionalGroup;

public class AdditionalGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinChoice { get; set; }
    public int MaxChoice { get; set; }
    public int Order { get; set; }
    public int ProductId { get; set; }
    public List<AdditionalDto> Additionals { get; set; } = default!;
}
