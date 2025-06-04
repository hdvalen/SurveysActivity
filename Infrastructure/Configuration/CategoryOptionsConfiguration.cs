
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CategoryOptionsConfiguration : IEntityTypeConfiguration<CategoryOptions>
    {
        public void Configure(EntityTypeBuilder<CategoryOptions> builder)
        {

            builder.ToTable("category_options");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(c => c.Created_At)
                .HasDefaultValueSql("now()");
                
            builder.Property(c => c.Updated_At)
                .HasDefaultValueSql("now()");
        }
    }
}