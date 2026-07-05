using Domain.Entities.Company.Product;
using Domain.Entities.Company.ProductCategory;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Base;

namespace Domain.Entities.Company.Affiliate;

public class CompanyAffiliate : BaseEntity<Guid>
{
    public Guid CompanyId { get; set; }
    public int LocationId { get; set; }
    public int WorkingHoursId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public TimeSpan AverageTimeCookingMin { get; set; }
    public TimeSpan AverageTimeCookingMax { get; set; }


    // conn
    public Company Company { get; set; }
    public CompanyAffiliateLocation Location { get; set; }
    public CompanyAffiliatesWorkingHour WorkingHours { get; set; }
    public ICollection<CompanyAffiliateProduct>? Products { get; set; }
    public ICollection<CompanyAffiliatesProductsCategory>? Categories { get; set; }
    public ICollection<UserCart>? Carts { get; set; }
}
