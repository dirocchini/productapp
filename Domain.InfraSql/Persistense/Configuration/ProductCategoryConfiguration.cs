using Domain.Entity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.InfraSqlServer.Persistense.Configuration;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategory");

        builder.HasKey(c => new { c.ProductId, c.CategoryId });

        builder
            .HasOne(c => c.Product)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(c => c.ProductId);

        builder
            .HasOne(c => c.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(c => c.CategoryId);
    }
}
