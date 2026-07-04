using Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class OrderProductAdditionalConfiguration
    : IEntityTypeConfiguration<OrderProductAdditional>
{
    public void Configure(EntityTypeBuilder<OrderProductAdditional> builder)
    {
        builder
            .HasOne(c => c.Product)
            .WithMany(c => c.AdditionalProducts)
            .HasForeignKey(c => c.OrderProductId);
        builder
            .HasOne(c => c.Additional)
            .WithMany(c => c.OrderedAdditionals)
            .HasForeignKey(c => c.AdditionalId);
    }
}
