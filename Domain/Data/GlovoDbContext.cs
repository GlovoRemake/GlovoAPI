using Core.Entities.Identity;
using Domain.Entities;
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
    }

    public DbSet<Region> Regions { get; set; }
    public DbSet<City> Cities { get; set; }

}