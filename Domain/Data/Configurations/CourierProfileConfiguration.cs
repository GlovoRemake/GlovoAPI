using Domain.Entities.Company.Product.Additional;
using Domain.Entities.Courier;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CourierProfileConfiguration
    : IEntityTypeConfiguration<CourierProfile>
{
    public void Configure(EntityTypeBuilder<CourierProfile> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithOne(u => u.CourierProfile)
            .HasForeignKey<CourierProfile>(c => c.UserId);
    }
}