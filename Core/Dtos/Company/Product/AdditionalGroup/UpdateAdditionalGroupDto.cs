using Core.Dtos.Company.Product.Additional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Product.AdditionalGroup;

public class UpdateAdditionalGroupDto
{
    public string Name { get; set; } = string.Empty;
    public int MinChoice { get; set; }
    public int MaxChoice { get; set; }
    public List<UpdateAdditionalDto> Additionals { get; set; } = default!;
}
