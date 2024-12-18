using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class TipoMedidaQuery : ITipoMedidaQuery
    {
        public readonly IngredientesDBContext _context;

        public TipoMedidaQuery(IngredientesDBContext context)
        {
            _context = context;
        }

        public TipoMedida GetTipoMedida(int tipoMedidaId)
        {
            var tm = _context.TiposMedida.FirstOrDefault(e => e.Id == tipoMedidaId);
            return tm;
        }

        public List<TipoMedida> GetListTipoMedida()
        {
            var tiposMedida = _context.TiposMedida.ToList();
            return tiposMedida;
        }
    }
}
