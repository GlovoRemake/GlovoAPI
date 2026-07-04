using Domain.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Domain.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .HasOne(c => c.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(c => c.ChatId);
        builder
            .HasOne(c => c.User)
            .WithMany(c => c.Messages)
            .HasForeignKey(c => c.UserId);
    }
}
