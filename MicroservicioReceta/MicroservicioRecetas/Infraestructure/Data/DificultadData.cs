using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data
{
    public class DificultadData : IEntityTypeConfiguration<Dificultad>
    {
        public void Configure(EntityTypeBuilder<Dificultad> builder)
        {
            builder.HasData(
            new Dificultad { DificultadId = 1, Nombre = "Principiante" },
            new Dificultad { DificultadId = 2, Nombre = "Fácil" },
            new Dificultad { DificultadId = 3, Nombre = "Media" },
            new Dificultad { DificultadId = 4, Nombre = "Díficil" },
            new Dificultad { DificultadId = 5, Nombre = "Avanzado" }
            );
        }
    }
}
