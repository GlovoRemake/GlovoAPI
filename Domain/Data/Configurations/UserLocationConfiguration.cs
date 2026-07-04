using Domain.Entities.Support;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Domain.Data.Configurations;

public class UserLocationConfiguration : IEntityTypeConfiguration<UserLocation>
{
    public void Configure(EntityTypeBuilder<UserLocation> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.UserLocations)
            .HasForeignKey(c => c.UserId);
    }
}
