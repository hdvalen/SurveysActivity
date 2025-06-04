
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CategoriesCatalogConfiguration : IEntityTypeConfiguration<CategoriesCatalog>
    {
        public void Configure(EntityTypeBuilder<CategoriesCatalog> builder)
        {

             builder.ToTable("categories_catalog");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(c => c.Created_At)
                .HasDefaultValueSql("now()");

            builder.Property(c => c.Updated_At)
                .HasDefaultValueSql("now()");

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(255);
        }
    }
}