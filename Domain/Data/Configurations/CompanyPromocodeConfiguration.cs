using Domain.Entities;
using Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class PromocodeCompanyConfiguration : IEntityTypeConfiguration<Promocode>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Promocode> builder)
    {
        builder
            .HasOne(x => x.Company)
            .WithMany(y => y.Promocodes)
            .HasForeignKey(x => x.CompanyId);
    }
}
