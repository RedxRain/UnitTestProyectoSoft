using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class IngredienteRecetaConfig : IEntityTypeConfiguration<IngredienteReceta>
    {
        public void Configure(EntityTypeBuilder<IngredienteReceta> entityBuilder)
        {
            entityBuilder.ToTable("IngredientesReceta");
            entityBuilder.HasKey(ir => ir.IngredienteRecetaId);
            entityBuilder.HasOne(r => r.Receta)
                .WithMany(ir => ir.IngredentesReceta)
                .HasForeignKey(r => r.RecetaId);
            entityBuilder.Property(ir => ir.cantidad)
                .IsRequired();
        }
    }
}
