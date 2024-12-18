using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class RecetaConfig : IEntityTypeConfiguration<Receta>
    {
        public void Configure(EntityTypeBuilder<Receta> entityBuilder)
        {
            entityBuilder.ToTable("Recetas");
            entityBuilder.HasKey(r => r.RecetaId);
            entityBuilder.HasOne(r => r.CategoriaReceta)
                .WithMany(cr => cr.Recetas)
                .HasForeignKey(r => r.CategoriaRecetaId)
                .IsRequired();
            entityBuilder.HasOne(r => r.Dificultad)
                .WithMany(d => d.Recetas)
                .HasForeignKey(r => r.DificultadId)
                .IsRequired();
            entityBuilder.Property(r => r.Titulo)
                .HasMaxLength(50)
                .IsRequired();
            entityBuilder.Property(r => r.FotoReceta)
                .HasMaxLength(250)
                .IsRequired();
            entityBuilder.Property(r => r.Video)
                .HasMaxLength(250)
                .IsRequired();
            entityBuilder.Property(r => r.TiempoPreparacion)
                .IsRequired();
            entityBuilder.Property(r => r.Topics)
                .HasMaxLength(255);
        }
    }
}
