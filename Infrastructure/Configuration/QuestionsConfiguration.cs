
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class QuestionsConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            
           builder.ToTable("questions");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(q => q.Created_At)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(q => q.Updated_At)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(q => q.Question_Number)
                .HasColumnName("question_number");

            builder.Property(q => q.Response_Type)
                .HasMaxLength(10)
                .HasColumnName("response_type");

            builder.Property(q => q.Comment_Question)
                .HasColumnName("comment_question");

            builder.Property(q => q.Question_Text)
                .HasColumnName("question_text")
                .HasColumnType("text")
                .IsRequired();
                
            builder.HasOne(q => q.Chapter)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.ChapterId);
        }
    }
}