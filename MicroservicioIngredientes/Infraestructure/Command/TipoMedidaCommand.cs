using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class TipoMedidaCommand : ITipoMedidaCommand
    {
        public readonly IngredientesDBContext _context;

        public TipoMedidaCommand(IngredientesDBContext context)
        {
            _context = context;
        }

        public async Task<TipoMedida> Insert(TipoMedida tipoMedida)
        {
            _context.TiposMedida.Add(tipoMedida);
            await _context.SaveChangesAsync();
            return tipoMedida;
        }

        public async Task<TipoMedida> Remove(int tipoMedidaId)
        {
            var tmAtDelete = await _context.TiposMedida.FirstOrDefaultAsync(e =>
                e.Id == tipoMedidaId);
            _context.TiposMedida.Remove(tmAtDelete);
            await _context.SaveChangesAsync();
            return tmAtDelete;
        }
    }
}
