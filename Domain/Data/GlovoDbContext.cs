using Core.Entities.Identity;
using Domain.Entities;
using Domain.Entities.Order;
using Domain.Entities.Support;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<Promocode> Promocodes { get; set; }
    public DbSet<UserOrder> UserOrders { get; set; }
    public DbSet<SupportChat> SupportChats { get; set; }
    public DbSet<Message> Messages { get; set; }
}