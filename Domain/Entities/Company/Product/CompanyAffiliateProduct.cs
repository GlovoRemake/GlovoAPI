using Domain.Entities.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.Product;

public class CompanyAffiliateProduct
{
    public int Id { get; set; }
    public Guid CompanyAffiliateId { get; set; }
    public int ProdcutId { get; set; }
    public bool IsAvailable { get; set; }

    // conn
    public CompanyAffiliate Affiliate { get; set; }
    public CompanyProduct Product { get; set; }
}
