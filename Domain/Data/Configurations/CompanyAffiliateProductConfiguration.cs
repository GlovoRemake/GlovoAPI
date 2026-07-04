using Domain.Entities.Company.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CompanyAffiliateProductConfiguration
    : IEntityTypeConfiguration<CompanyAffiliateProduct>
{
    public void Configure(EntityTypeBuilder<CompanyAffiliateProduct> builder)
    {
        builder
            .HasOne(c => c.Product)
            .WithMany(c => c.Affiliates)
            .HasForeignKey(c => c.ProdcutId);
        builder
            .HasOne(c => c.Affiliate)
            .WithMany(c => c.Products)
            .HasForeignKey(c => c.CompanyAffiliateId);
    }
}
