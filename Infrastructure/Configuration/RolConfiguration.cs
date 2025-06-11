using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("rols");
            builder.HasKey(r => r.Id); // Establece la clave primaria
            builder.Property(p => p.Id)
                    .IsRequired();
            builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(80);
        }
    }
}