using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Querys
{
    public class IngredienteQuery : IIngredienteQuery
    {
        public readonly IngredientesDBContext _context;

        public IngredienteQuery(IngredientesDBContext context)
        {
            _context = context;
        }

        public Ingrediente GetById(int ingredienteId)
        {
            var ingrediente = _context.Ingredientes
                .Include(e => e.TipoMedida)
                .Include(e => e.TipoIngrediente)
                .FirstOrDefault(e => e.Id == ingredienteId);
            return ingrediente;
        }

        public async Task<List<Ingrediente>> GetAll()
        {
            var ingredientes = await _context.Ingredientes.Include(e => e.TipoIngrediente).Include(e => e.TipoMedida).ToListAsync();
            return ingredientes;
        }
    }
}
