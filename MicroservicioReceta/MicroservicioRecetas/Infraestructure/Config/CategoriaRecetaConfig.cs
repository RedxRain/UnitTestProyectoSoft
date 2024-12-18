using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class CategoriaRecetaConfig : IEntityTypeConfiguration<CategoriaReceta>
    {
        public void Configure(EntityTypeBuilder<CategoriaReceta> entityBuilder)
        {
            entityBuilder.ToTable("CategoriasReceta");
            entityBuilder.HasKey(cr => cr.CategoriaRecetaId);
            entityBuilder.HasMany(r => r.Recetas)
                .WithOne(cr => cr.CategoriaReceta);
            entityBuilder.Property(cr => cr.Nombre)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
