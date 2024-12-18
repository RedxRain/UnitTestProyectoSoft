using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data
{
    public class CategoriaRecetaData : IEntityTypeConfiguration<CategoriaReceta>
    {
        public void Configure(EntityTypeBuilder<CategoriaReceta> builder)
        {
            builder.HasData(
                new CategoriaReceta { CategoriaRecetaId = 1, Nombre = "Pastas" },
                new CategoriaReceta { CategoriaRecetaId = 2, Nombre = "Minutas" },
                new CategoriaReceta { CategoriaRecetaId = 3, Nombre = "Ensaladas" },
                new CategoriaReceta { CategoriaRecetaId = 4, Nombre = "Pescado" },
                new CategoriaReceta { CategoriaRecetaId = 5, Nombre = "Carne" },
                new CategoriaReceta { CategoriaRecetaId = 6, Nombre = "Vegetariana" },
                new CategoriaReceta { CategoriaRecetaId = 7, Nombre = "Sopas" },
                new CategoriaReceta { CategoriaRecetaId = 8, Nombre = "Postres" },
                new CategoriaReceta { CategoriaRecetaId = 9, Nombre = "Desayunos" },
                new CategoriaReceta { CategoriaRecetaId = 10, Nombre = "Bebidas" },
                new CategoriaReceta { CategoriaRecetaId = 11, Nombre = "Aperitivos" }
            );
        }
    }
}
