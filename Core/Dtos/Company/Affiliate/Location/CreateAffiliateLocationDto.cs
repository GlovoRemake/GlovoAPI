using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dtos.Company.Affiliate.Location;

public class CreateAffiliateLocationDto
{
    public string Location { get; set; } = string.Empty;
    public int RegionId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PostalIndex { get; set; } = string.Empty;
}
