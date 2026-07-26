using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company;

public class AddRequestCompanyDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
