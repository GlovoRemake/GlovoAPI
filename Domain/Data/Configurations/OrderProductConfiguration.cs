using Domain.Entities.Order;
using Domain.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder
            .HasOne(op => op.Order)
            .WithMany(u => u.Products)
            .HasForeignKey(op => op.OrderId);

        builder
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderedProducts)
            .HasForeignKey(op => op.ProductId);
    }
}