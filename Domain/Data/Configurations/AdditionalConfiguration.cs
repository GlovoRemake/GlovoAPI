using Domain.Entities.Company.Product.Additional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class AdditionalConfiguration
    : IEntityTypeConfiguration<Additional>
{
    public void Configure(EntityTypeBuilder<Additional> builder)
    {
        builder
            .HasOne(c => c.Group)
            .WithMany(c => c.Additionals)
            .HasForeignKey(c => c.AdditionalGroupId);
    }
}