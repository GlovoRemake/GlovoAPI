using Domain.Entities.Company.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class CompanyProductConfiguration : IEntityTypeConfiguration<CompanyProduct>
{
    public void Configure(EntityTypeBuilder<CompanyProduct> builder)
    {
        builder
            .HasOne(c => c.Company)
            .WithMany(co => co.Products)
            .HasForeignKey(c => c.CompanyId);
        builder
            .HasOne(c => c.Category)
            .WithMany(co => co.Products)
            .HasForeignKey(c => c.CategoryId);
    }
}
