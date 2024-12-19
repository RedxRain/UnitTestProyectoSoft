using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace Infraestructure.Data
{
    internal class IngredienteSeeder : IEntityTypeConfiguration<Ingrediente>
    {
        public void Configure(EntityTypeBuilder<Ingrediente> builder)
        {
            builder.HasData(Seeder());
        }

        public List<Ingrediente> Seeder()
        {
            string csvIngredientes = "TodosLosIngredientes.csv";
            string csvArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, csvIngredientes);
            List<Ingrediente> list = new List<Ingrediente>();
            int id = 1;

            using (var reader = new StreamReader(csvArchivo))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                while (csv.Read())
                {
                    // Obtener los campos de la fila
                    string nombre = csv.GetField<string>(0).ToString();
                    int tipoMedidaId = csv.GetField<int>(1);
                    int tipoIngredienteId = csv.GetField<int>(2);

                    // Crear un nuevo Ingrediente
                    var ingrediente = new Ingrediente
                    {
                        Id = id++,
                        Name = nombre,
                        TipoMedidaID = tipoMedidaId,
                        TipoIngredienteID = tipoIngredienteId
                    };

                    list.Add(ingrediente);
                }
            }

            return list;
        }
    }
}
