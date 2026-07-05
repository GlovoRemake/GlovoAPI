using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.Affiliate;

public class CompanyAffiliateLocation
{
    public int Id { get; set; }
    public string Location { get; set; }
    public int RegionId { get; set; }
    public string Address { get; set; }
    public string PostalIndex { get; set; }

    // conn
    public Region Region { get; set; }
    public ICollection<CompanyAffiliate> Affiliates { get; set; }
}
