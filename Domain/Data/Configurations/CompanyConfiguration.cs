using Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Company> builder)
    {
        builder
            .HasOne(c => c.Owner)
            .WithMany(c => c.Companies)
            .HasForeignKey(c => c.OwnerId);
    }
}
