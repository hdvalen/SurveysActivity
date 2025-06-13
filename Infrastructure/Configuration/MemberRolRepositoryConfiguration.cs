using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class MemberRolRepositoryConfiguration : IEntityTypeConfiguration<MemberRols>
    {
        public void Configure(EntityTypeBuilder<MemberRols> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("members_rols");
            builder.HasKey(e => new { e.MemberId, e.RolId }); // Asumiendo que 'Id' es la clave primaria

            builder.HasOne(p => p.Rol)
            .WithMany(p => p.MembersRols)
            .HasForeignKey(p => p.RolId);

            builder.HasOne(p => p.Member)
            .WithMany(p => p.MemberRols)
            .HasForeignKey(p => p.MemberId);

        }
    }
}