using Domain.Entities.Company.ProductCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CompanyProductCategoryConfiguration
    : IEntityTypeConfiguration<CompanyProductCategory>
{
    public void Configure(EntityTypeBuilder<CompanyProductCategory> builder)
    {
        builder
            .HasOne(c => c.Company)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(c => c.CompanyId);
    }
}
