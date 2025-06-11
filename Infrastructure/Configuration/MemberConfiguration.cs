using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<UserMember>
    {
        public void Configure(EntityTypeBuilder<UserMember> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("members");
            builder.HasKey(e => e.Id); // Asumiendo que 'Id' es la clave primaria
            builder.Property(p => p.Id)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.Property(p => p.Username)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.HasMany(p => p.RefreshTokens)
                    .WithOne(p => p.Users)
                    .HasForeignKey(p => p.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}