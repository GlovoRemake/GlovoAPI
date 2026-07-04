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
        // Fluent api conf
        modelBuilder.Entity<City>()
            .HasOne(c => c.Region)
            .WithMany(c => c.Cities)
            .HasForeignKey(c => c.RegionId);

        modelBuilder.Entity<CourierTimeSlot>()
            .HasOne(c => c.City)
            .WithMany(c => c.CourierTimeSlots)
            .HasForeignKey(c => c.CityId);
        modelBuilder.Entity<CourierTimeSlot>()
            .HasOne(c => c.User)
            .WithMany(c => c.CourierTimeSlots)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<UserLocation>()
            .HasOne(c => c.User)
            .WithMany(c => c.UserLocations)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<UserOrder>()
            .HasOne(c => c.User)
            .WithMany(c => c.UserOrders)
            .HasForeignKey(c => c.UserId);
        modelBuilder.Entity<UserOrder>()
            .HasOne(c => c.UserLocation)
            .WithMany(c => c.UserOrders)
            .HasForeignKey(c => c.LocationId);

        modelBuilder.Entity<SupportChat>()
            .HasOne(c => c.User)
            .WithMany(u => u.UserSupportChats)
            .HasForeignKey(c => c.UserId);
        modelBuilder.Entity<SupportChat>()
            .HasOne(c => c.Support)
            .WithMany(u => u.AssignedSupportChats)
            .HasForeignKey(c => c.SupportId);
        modelBuilder.Entity<SupportChat>()
            .HasOne(c => c.Order)
            .WithOne(c => c.SupportChat)
            .HasForeignKey<SupportChat>(c => c.OrderId);

        modelBuilder.Entity<Message>()
            .HasOne(c => c.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(c => c.ChatId);
        modelBuilder.Entity<Message>()
            .HasOne(c => c.User)
            .WithMany(c => c.Messages)
            .HasForeignKey(c => c.UserId);
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