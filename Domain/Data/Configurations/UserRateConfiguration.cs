using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class UserRateConfiguration : IEntityTypeConfiguration<UserRate>
{
    public void Configure(EntityTypeBuilder<UserRate> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.Rates)
            .HasForeignKey(c => c.UserId);
        builder
            .HasOne(c => c.Order)
            .WithMany(c => c.Rates)
            .HasForeignKey(c => c.OrderId);
        builder
            .HasOne(c => c.Company)
            .WithMany(c => c.Rates)
            .HasForeignKey(c => c.CompanyId);
    }
}
