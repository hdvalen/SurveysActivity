
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class OptionsResponseConfiguration : IEntityTypeConfiguration<OptionsResponse>
    {
        public void Configure(EntityTypeBuilder<OptionsResponse> builder)
        {
            
           builder.ToTable("options_response");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(o => o.Created_At)
                .HasDefaultValueSql("now()");

            builder.Property(o => o.Updated_At)
                .HasDefaultValueSql("now()");

            builder.Property(o => o.OptionQuestionId)
                .HasColumnName("option_question_id")
                .IsRequired();
        }
    }
}