
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SummaryOptionConfiguration : IEntityTypeConfiguration<SummaryOptions>
    {
        public void Configure(EntityTypeBuilder<SummaryOptions> builder)
        {
            
            
            builder.ToTable("summaryoptions");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd().IsRequired().HasColumnName("id");
            builder.Property(s => s.Code_Number).HasColumnName("code_number").HasMaxLength(20);
            builder.Property(s => s.ValueRta).HasColumnName("value_rta").HasColumnType("text");
        }
    }
}