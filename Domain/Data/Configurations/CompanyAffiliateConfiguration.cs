using Domain.Entities.Company.Affiliate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CompanyAffiliateConfiguration : IEntityTypeConfiguration<CompanyAffiliate>
{
    public void Configure(EntityTypeBuilder<CompanyAffiliate> builder)
    {
        builder
            .HasOne(c => c.Company)
            .WithMany(c => c.Affiliates)
            .HasForeignKey(c => c.CompanyId);
        builder
            .HasOne(c => c.Location)
            .WithMany(c => c.Affiliates)
            .HasForeignKey(c => c.LocationId);
        builder
            .HasOne(c => c.WorkingHours)
            .WithMany(c => c.Affiliates)
            .HasForeignKey(c => c.WorkingHoursId);
    }
}
