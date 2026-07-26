using Domain.Entities.Company;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class RequestCompanyConfiguration
    : IEntityTypeConfiguration<RequestCompany>
{
    public void Configure(EntityTypeBuilder<RequestCompany> builder)
    {
        builder
            .HasOne(c => c.Partner)
            .WithMany(c => c.RequestCompanies)
            .HasForeignKey(c => c.PartnerId);

        builder
            .HasOne(c => c.Company)
            .WithMany(c => c.RequestCompanies)
            .HasForeignKey(c => c.CompanyId);
    }
}