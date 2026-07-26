using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? IconPath { get; set; }
    public string? BannerPath { get; set; }
    public Guid OwnerId { get; set; }
}
