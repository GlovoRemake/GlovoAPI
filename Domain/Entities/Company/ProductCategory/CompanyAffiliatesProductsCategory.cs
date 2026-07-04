using Domain.Entities.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.ProductCategory;

public class CompanyAffiliatesProductsCategory
{
    public int Id { get; set; }
    public Guid CompanyAffiliateId { get; set; }
    public int CategoryId { get; set; }


    // conn
    public CompanyAffiliate Affiliate { get; set; }
    public CompanyProductCategory Category { get; set; }
}
