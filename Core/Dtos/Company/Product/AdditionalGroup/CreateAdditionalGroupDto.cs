using Core.Dtos.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Product.AdditionalGroup;

public class CreateAdditionalGroupDto
{
    public string Name { get; set; } = string.Empty;
    public int MinChoice { get; set; }
    public int MaxChoice { get; set; }
    public List<CreateAdditionalDto> Additionals { get; set; } = default!;
}
