using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company;

public class RequestCompanyDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? IsApprove { get; set; }
    public string? Message { get; set; }
    public Guid PartnerId { get; set; }
    public Guid? CompanyId { get; set; }
}
