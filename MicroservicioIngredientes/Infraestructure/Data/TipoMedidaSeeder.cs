using CsvHelper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace Infraestructure.Data
{
    internal class TipoMedidaSeeder : IEntityTypeConfiguration<TipoMedida>
    {
        public void Configure(EntityTypeBuilder<TipoMedida> builder)
        {

            builder.HasData(Seeder());
        }

        public List<TipoMedida> Seeder()
        {
            string csvTipoMedida = "TodosLosTiposDeMedida.csv";
            string csvArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, csvTipoMedida);
            List<TipoMedida> list = new List<TipoMedida>();
            int id = 1;

            using (var reader = new StreamReader(csvArchivo))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    var tipoMedida = new TipoMedida
                    {
                        Id = id++,
                        Name = csv.GetField<string>(0) // El índice 0 representa la primera columna en el CSV
                    };

                    list.Add(tipoMedida);
                }
            }

            return list;
        }
    }
}
