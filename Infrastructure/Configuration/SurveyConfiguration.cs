
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            
            builder.ToTable("surveys");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(s => s.Created_At)
                .HasDefaultValueSql("now()");

            builder.Property(s => s.Updated_At)
                .HasDefaultValueSql("now()");

            builder.Property(s => s.ComponentHtml)
                .HasMaxLength(20)
                .HasColumnName("componenthtml");

            builder.Property(s => s.ComponentReact)
                .HasMaxLength(20)
                .HasColumnName("componentreact");

            builder.Property(s => s.Description)
                .HasColumnType("text");
                
            builder.Property(s => s.Instruction)
                .HasColumnType("text");
            builder.Property(s => s.Name)
                .HasColumnType("text")
                .IsRequired();
        }
    }
}