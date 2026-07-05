using Core.Entities.Identity;
using Domain.Entities;
using Domain.Entities.Company;
using Domain.Entities.Company.Affiliate;
using Domain.Entities.User;
using Domain.Entities.Order;
using Domain.Entities.Support;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Company.Partner;
using Domain.Entities.Company.ProductCategory;
using Domain.Entities.Company.Product;
using Domain.Entities.Company.Product.Additional;

namespace Domain.Data;

public class GlovoDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public GlovoDbContext(DbContextOptions<GlovoDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GlovoDbContext).Assembly);
    }

    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<CourierTimeSlot> CourierTimeSlots { get; set; }
    public DbSet<UserLocation> UserLocations { get; set; }
    public DbSet<UserRate> UserRates { get; set; }
    public DbSet<Promocode> Promocodes { get; set; }
    public DbSet<UserOrder> UserOrders { get; set; }
    public DbSet<UserCart> UserCarts { get; set; }
    public DbSet<UserCartAdditional> UserCartsAdditionals { get; set; }
    public DbSet<SupportChat> SupportChats { get; set; }
    public DbSet<Message> Messages { get; set; }



    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyType> CompanyTypes { get; set; }
    public DbSet<CompanyProductCategory> CompanyProductCategories { get; set; }
    public DbSet<CompanyProduct> CompanyProducts { get; set; }
    public DbSet<AdditionalGroup> AdditionalGroups { get; set; }
    public DbSet<Additional> Additionals { get; set; }
    public DbSet<PartnerUser> PartnerUsers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<PartnerRole> PartnerRoles { get; set; }
    public DbSet<CompanyAffiliate> CompanyAffiliates { get; set; }
    public DbSet<CompanyAffiliateProduct> CompanyAffiliatesProducts { get; set; }
    public DbSet<CompanyAffiliatesProductsCategory> CompanyAffiliatesProductsCategories { get; set; }
    public DbSet<CompanyAffiliateLocation> CompanyAffiliateLocations { get; set; }
    public DbSet<CompanyAffiliatesWorkingHour> CompanyAffiliatesWorkingHours { get; set; }
}