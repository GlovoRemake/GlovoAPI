using Domain.Entities.Company.Product.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Data.Configurations;

public class AdditionalSaleConfiguration : IEntityTypeConfiguration<AdditionalSale>
{
    public void Configure(EntityTypeBuilder<AdditionalSale> builder)
    {
        builder
            .HasOne(op => op.Additional)
            .WithMany(u => u.Sales)
            .HasForeignKey(op => op.AdditionalId);
    }
}