using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class TipoIngredienteConfig : IEntityTypeConfiguration<TipoIngrediente>
    {
        public void Configure(EntityTypeBuilder<TipoIngrediente> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
            entityBuilder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        }
    }
}
