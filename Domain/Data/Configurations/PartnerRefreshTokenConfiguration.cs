using Domain.Entities.Company.Partner;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class PartnerRefreshTokenConfiguration
    : IEntityTypeConfiguration<PartnerRefreshToken>
{
    public void Configure(EntityTypeBuilder<PartnerRefreshToken> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.RefreshTokens)
            .HasForeignKey(c => c.UserId);
    }
}