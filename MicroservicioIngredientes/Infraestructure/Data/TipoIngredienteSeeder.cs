using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace Infraestructure.Data
{
    internal class TipoIngredienteSeeder : IEntityTypeConfiguration<TipoIngrediente>
    {
        public void Configure(EntityTypeBuilder<TipoIngrediente> builder)
        {
            builder.HasData(Seeder());
        }

        public List<TipoIngrediente> Seeder()
        {
            string csvTipoIngrediente = "TodosLosTiposDeIngredientes.csv";
            string csvArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, csvTipoIngrediente);
            List<TipoIngrediente> list = new List<TipoIngrediente>();
            int id = 1;

            using (var reader = new StreamReader(csvArchivo))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                while (csv.Read())
                {
                    var tipoIngrediente = new TipoIngrediente
                    {
                        Id = id++,
                        Name = csv.GetField<string>(0) // El indice 0 representa la primera columna en el CSV
                    };

                    list.Add(tipoIngrediente);
                }
            }

            return list;
        }
    }
}
