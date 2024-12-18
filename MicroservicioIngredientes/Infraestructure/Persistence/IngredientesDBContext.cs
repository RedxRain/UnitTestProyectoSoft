using Domain.Entities;
using Infraestructure.Config;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class IngredientesDBContext : DbContext
    {
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<TipoIngrediente> TiposIngrediente { get; set; }
        public DbSet<TipoMedida> TiposMedida { get; set; }

        public IngredientesDBContext(DbContextOptions<IngredientesDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoMedidaConfig());
            modelBuilder.ApplyConfiguration(new TipoMedidaSeeder());

            modelBuilder.ApplyConfiguration(new TipoIngredienteConfig());
            modelBuilder.ApplyConfiguration(new TipoIngredienteSeeder());

            modelBuilder.ApplyConfiguration(new IngredienteConfig());
            modelBuilder.ApplyConfiguration(new IngredienteSeeder());
        }
    }
}
