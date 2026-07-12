using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = Domain.Entities.Company.Type.Type;

namespace Domain.Data.Configurations;

public class TypeConfiguration : IEntityTypeConfiguration<Type>
{
    public void Configure(EntityTypeBuilder<Type> builder)
    {
        builder
            .HasOne(c => c.ParentType)
            .WithMany(u => u.Types)
            .HasForeignKey(c => c.ParentTypeId);
    }
}
