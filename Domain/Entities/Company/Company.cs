using Domain.Entities.Company.Affiliate;
using Domain.Entities.Company.Partner;
using Domain.Entities.Company.Product;
using Domain.Entities.Company.ProductCategory;
using Domain.Entities.User;

namespace Domain.Entities.Company;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string IconPath { get; set; }
    public string BannerPath { get; set; }
    public Guid OwnerId { get; set; }
    public int TypeId { get; set; }

    // conn
    public CompanyType Type { get; set; }
    public PartnerUser Owner { get; set; }
    public ICollection<CompanyAffiliate>? Affiliates { get; set; }
    public ICollection<UserRate>? Rates { get; set; }
    public ICollection<CompanyProduct>? Products { get; set; }
    public ICollection<CompanyProductCategory>? ProductCategories { get; set; }
}
