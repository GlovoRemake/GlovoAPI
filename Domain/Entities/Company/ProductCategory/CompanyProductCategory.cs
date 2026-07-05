using Domain.Entities.Company.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Company.ProductCategory;

public class CompanyProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public Guid CompanyId { get; set; }

    // conn
    public Company Company { get; set; }
    public ICollection<CompanyProduct> Products { get; set; }
    public ICollection<CompanyAffiliatesProductsCategory> Affiliates { get; set; }
}
