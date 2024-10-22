using Domain.Entity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.InfraSqlServer.Persistense.Configuration;

internal class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Category");

        builder.HasIndex(c=>c.Name)
            .IsUnique();

        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasData(
            new CategoryEntity() { Id = 1, Name = "Esportes", DateAdded = new DateTime(2024,10,21,11,11,01), DateUpdated = new DateTime(2024,10,21,11,11,01) },
            new CategoryEntity() { Id = 2, Name = "Roupas", DateAdded = new DateTime(2024,10,21,11,11,01), DateUpdated = new DateTime(2024,10,21,11,11,01) },
            new CategoryEntity() { Id = 3, Name = "Mesa e Banho", DateAdded = new DateTime(2024,10,21,11,11,01), DateUpdated = new DateTime(2024,10,21,11,11,01) },
            new CategoryEntity() { Id = 4, Name = "Moto", DateAdded = new DateTime(2024,10,21,11,11,01), DateUpdated = new DateTime(2024,10,21,11,11,01) }
        );
    }
}