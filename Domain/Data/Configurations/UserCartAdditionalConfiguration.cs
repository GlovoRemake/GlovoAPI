using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class UserCartAdditionalConfiguration
    : IEntityTypeConfiguration<UserCartAdditional>
{
    public void Configure(EntityTypeBuilder<UserCartAdditional> builder)
    {
        builder
            .HasOne(c => c.Cart)
            .WithMany(c => c.Additionals)
            .HasForeignKey(c => c.CartId);
        builder
           .HasOne(c => c.Product)
           .WithMany(c => c.Additionals)
           .HasForeignKey(c => c.ProductId);
        builder
           .HasOne(c => c.Additional)
           .WithMany(c => c.Additionals)
           .HasForeignKey(c => c.AdditionalId);
    }
}
