using Domain.Entities.Company.Product.Sale;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Data.Configurations;

public class ProductSaleConfiguration : IEntityTypeConfiguration<ProductSale>
{
    public void Configure(EntityTypeBuilder<ProductSale> builder)
    {
        builder
            .HasOne(op => op.Product)
            .WithMany(u => u.Sales)
            .HasForeignKey(op => op.CompanyProductId);
    }
}