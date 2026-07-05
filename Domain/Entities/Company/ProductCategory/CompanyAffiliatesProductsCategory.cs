using Domain.Entities.Company.Affiliate;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.ProductCategory;

public class CompanyAffiliatesProductsCategory : BaseEntityWithIsDeleted<int>
{
    public Guid CompanyAffiliateId { get; set; }
    public int CategoryId { get; set; }


    // conn
    public CompanyAffiliate Affiliate { get; set; }
    public CompanyProductCategory Category { get; set; }
}
