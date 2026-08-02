using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Affiliate;

public class PagedAffiliatesDto
{
    public List<AffiliateDto> Affiliates { get; set; } = [];
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
