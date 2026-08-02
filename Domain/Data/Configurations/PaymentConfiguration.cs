using Domain.Entities;
using Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class PaymentConfiguration
    : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder
            .HasOne(c => c.Order)
            .WithMany(c => c.Payments)
            .HasForeignKey(c => c.OrderId);
        builder
            .HasOne(c => c.Courier)
            .WithMany(c => c.Payments)
            .HasForeignKey(c => c.CourierId);
    }
}