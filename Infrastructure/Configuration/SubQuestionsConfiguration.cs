
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SubQuestionsConfiguration : IEntityTypeConfiguration<SubQuestion>
    {
        public void Configure(EntityTypeBuilder<SubQuestion> builder)
        {
            builder.ToTable("sub_questions");
            builder.HasKey(sq => sq.Id);
            builder.Property(sq => sq.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(sq => sq.Created_At)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(sq => sq.Updated_At)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(sq => sq.SubquestionNumber)
                .HasColumnName("subquestion_number");

            builder.Property(sq => sq.CommentSubquestion)
                .HasColumnName("comment_subquestion");
                
            builder.Property(sq => sq.SubquestionText)
                .HasColumnName("subquestiontext")
                .HasColumnType("text")
                .IsRequired();
        }
    }
}