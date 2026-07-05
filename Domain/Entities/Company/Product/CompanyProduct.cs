using Domain.Entities.Company.Product.Additional;
using Domain.Entities.Company.ProductCategory;
using Domain.Entities.Order;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Product;

public class CompanyProduct : BaseEntityWithIsDeleted<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public int Order { get; set; }
    public double Price { get; set; }
    public Guid CompanyId { get; set; }
    public int CategoryId { get; set; }
    public double Weight { get; set; }
    public double Kcal { get; set; }

    // conn
    public Company Company { get; set; }
    public CompanyProductCategory Category { get; set; }
    public ICollection<CompanyAffiliateProduct>? Affiliates { get; set; }
    public ICollection<AdditionalGroup>? AdditionalGroups { get; set; }
    public ICollection<UserCart>? Carts { get; set; }
    public ICollection<UserCartAdditional>? Additionals { get; set; }
    public ICollection<OrderProduct> OrderedProducts { get; set; }
}
