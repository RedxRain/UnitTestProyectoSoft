using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class IngredienteConfig : IEntityTypeConfiguration<Ingrediente>
    {
        public void Configure(EntityTypeBuilder<Ingrediente> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
            entityBuilder.Property(e => e.Name).HasMaxLength(50).IsRequired();

            entityBuilder.
            HasOne<TipoIngrediente>(e => e.TipoIngrediente)
            .WithMany(ti => ti.Ingredientes)
            .HasForeignKey(e => e.TipoIngredienteID)
            .IsRequired();

            entityBuilder.
            HasOne<TipoMedida>(e => e.TipoMedida)
            .WithMany(tm => tm.Ingredientes)
            .HasForeignKey(e => e.TipoMedidaID)
            .IsRequired();
        }
    }
}
