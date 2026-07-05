using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Affiliate;

public class CompanyAffiliateLocation : BaseEntity<int>
{
    public string Location { get; set; }
    public int RegionId { get; set; }
    public string Address { get; set; }
    public string PostalIndex { get; set; }

    // conn
    public Region Region { get; set; }
    public ICollection<CompanyAffiliate> Affiliates { get; set; }
}
