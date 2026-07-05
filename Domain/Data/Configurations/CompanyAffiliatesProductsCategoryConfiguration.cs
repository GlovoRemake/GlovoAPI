using Domain.Entities.Company.ProductCategory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CompanyAffiliatesProductsCategoryConfiguration
    : IEntityTypeConfiguration<CompanyAffiliatesProductsCategory>
{
    public void Configure(EntityTypeBuilder<CompanyAffiliatesProductsCategory> builder)
    {
        builder
            .HasOne(c => c.Affiliate)
            .WithMany(c => c.Categories)
            .HasForeignKey(c => c.CompanyAffiliateId);
        builder
            .HasOne(c => c.Category)
            .WithMany(c => c.Affiliates)
            .HasForeignKey(c => c.CategoryId);
    }
}
