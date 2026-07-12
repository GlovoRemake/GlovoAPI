using Domain.Entities.Company.Type;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = System.Type;

namespace Domain.Data.Configurations;

public class CompanyTypeConfiguration : IEntityTypeConfiguration<CompanyType>
{
    public void Configure(EntityTypeBuilder<CompanyType> builder)
    {
        builder
            .HasOne(c => c.Type)
            .WithMany(u => u.CompanyTypes)
            .HasForeignKey(c => c.TypeId);
        
        builder
            .HasOne(c => c.Company)
            .WithMany(u => u.Types)
            .HasForeignKey(c => c.CompanyId);
    }
}
