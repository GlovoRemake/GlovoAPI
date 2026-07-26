using Domain.Entities.Base;
using Domain.Entities.Company.Partner;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Entities.Company;

public class RequestCompany : BaseEntity<long>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? IsApprove { get; set; }
    public string? Message { get; set; }
    public Guid PartnerId { get; set; }
    public Guid? CompanyId { get; set; }


    // conn
    public PartnerUser Partner { get; set; } = default!;
    public Company? Company { get; set; }
}
