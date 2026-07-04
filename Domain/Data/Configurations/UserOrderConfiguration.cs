using Domain.Entities;
using Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Domain.Data.Configurations;

public class UserOrderConfiguration : IEntityTypeConfiguration<UserOrder>
{
    public void Configure(EntityTypeBuilder<UserOrder> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.UserOrders)
            .HasForeignKey(c => c.UserId);
        builder
            .HasOne(c => c.UserLocation)
            .WithMany(c => c.UserOrders)
            .HasForeignKey(c => c.LocationId);
        builder
            .HasOne(c => c.Promocode)
            .WithMany(c => c.UserOrders)
            .HasForeignKey(c => c.PromocodeId);
    }
}