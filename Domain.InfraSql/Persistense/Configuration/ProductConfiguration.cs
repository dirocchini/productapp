using Domain.Entity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.InfraSqlServer.Persistense.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Product");

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Value)
            .HasColumnType("decimal(10,4)")
            .IsRequired();
    }
}
