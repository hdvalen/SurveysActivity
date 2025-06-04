
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class OptionsQuestionConfiguration : IEntityTypeConfiguration<OptionQuestion>
    {
        public void Configure(EntityTypeBuilder<OptionQuestion> builder)
        {
            
           builder.ToTable("option_questions");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(o => o.Created_At)
                .HasDefaultValueSql("now()");

            builder.Property(o => o.Updated_At)
                .HasDefaultValueSql("now()");

            builder.Property(o => o.CommentOptiones)
                .HasColumnName("comment_options")
                .HasColumnType("text");
                
            builder.Property(o => o.NumberOption)
                .HasColumnName("numberoption")
                .HasMaxLength(20);
        }
    }
}