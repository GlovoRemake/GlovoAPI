using Domain.Entities.Support;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class UserCartConfiguration
    : IEntityTypeConfiguration<UserCart>
{
    public void Configure(EntityTypeBuilder<UserCart> builder)
    {
        builder
            .HasOne(c => c.Product)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.ProductId);
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId);
        builder
            .HasOne(c => c.Affiliate)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.AffiliateId);
    }
}
