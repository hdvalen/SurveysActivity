
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ChaptersConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {

             builder.ToTable("chapters");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(c => c.Created_At).HasDefaultValueSql("now()");

            builder.Property(c => c.Updated_At).HasDefaultValueSql("now()");

            builder.Property(c => c.ComponentHtml)
                .HasColumnName("componenthtml")
                .HasMaxLength(20);

            builder.Property(c => c.ComponentReact)
                .HasColumnName("componentreact")
                .HasMaxLength(20);

            builder.Property(c => c.Chapter_Number)
                .HasColumnName("chapter_number")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Chapter_Title)
                .HasColumnName("chapter_title")
                .IsRequired();

            builder.HasMany(c => c.Questions)
               .WithOne(q => q.Chapter)
               .HasForeignKey(q => q.ChapterId);
        }
    }
}