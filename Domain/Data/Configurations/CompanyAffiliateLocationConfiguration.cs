using Domain.Entities.Company.Affiliate;
using Domain.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CompanyAffiliateLocationConfiguration
    : IEntityTypeConfiguration<CompanyAffiliateLocation>
{
    public void Configure(EntityTypeBuilder<CompanyAffiliateLocation> builder)
    {
        builder
            .HasOne(c => c.Region)
            .WithMany(c => c.CompanyAffiliateLocations)
            .HasForeignKey(c => c.RegionId);
    }
}
