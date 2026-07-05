using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CourierTimeSlotConfiguration : IEntityTypeConfiguration<CourierTimeSlot>
{
    public void Configure(EntityTypeBuilder<CourierTimeSlot> builder)
    {
        builder
            .HasOne(c => c.City)
            .WithMany(c => c.CourierTimeSlots)
            .HasForeignKey(c => c.CityId);

        builder
            .HasOne(c => c.User)
            .WithMany(c => c.CourierTimeSlots)
            .HasForeignKey(c => c.UserId);
    }
}