using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class TipoIngredienteCommand : ITipoIngredienteCommand
    {
        public readonly IngredientesDBContext _context;

        public TipoIngredienteCommand(IngredientesDBContext context)
        {
            _context = context;
        }

        public async Task<TipoIngrediente> Insert(TipoIngrediente tipoIngrediente)
        {
            _context.TiposIngrediente.Add(tipoIngrediente);
            await _context.SaveChangesAsync();
            return tipoIngrediente;
        }

        public async Task<TipoIngrediente> Remove(int tipoIngredienteId)
        {
            var tiAtDelete = await _context.TiposIngrediente.FirstOrDefaultAsync(e =>
                e.Id == tipoIngredienteId);
            _context.TiposIngrediente.Remove(tiAtDelete);
            await _context.SaveChangesAsync();
            return tiAtDelete;
        }

        public Task<TipoIngrediente> Update(TipoIngrediente tipoIngrediente)
        {
            throw new NotImplementedException();
        }
    }
}
