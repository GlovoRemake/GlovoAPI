using Domain.Entities.Company.Product.Additional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class AdditionalGroupConfiguration
    : IEntityTypeConfiguration<AdditionalGroup>
{
    public void Configure(EntityTypeBuilder<AdditionalGroup> builder)
    {
        builder
            .HasOne(c => c.Product)
            .WithMany(c => c.AdditionalGroups)
            .HasForeignKey(c => c.ProductId);
    }
}
