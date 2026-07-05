using Domain.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data.Configurations;

public class SupportChatConfiguration : IEntityTypeConfiguration<SupportChat>
{
    public void Configure(EntityTypeBuilder<SupportChat> builder)
    {
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.UserSupportChats)
            .HasForeignKey(c => c.UserId);

        builder
            .HasOne(c => c.Support)
            .WithMany(u => u.AssignedSupportChats)
            .HasForeignKey(c => c.SupportId);

        builder
            .HasOne(c => c.Order)
            .WithOne(c => c.SupportChat)
            .HasForeignKey<SupportChat>(c => c.OrderId);
    }
}
