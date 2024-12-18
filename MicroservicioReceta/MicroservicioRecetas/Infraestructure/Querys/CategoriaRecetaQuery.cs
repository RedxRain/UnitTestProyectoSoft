using Application.Interfaces.Querys;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Querys
{
    public class CategoriaRecetaQuery : ICategoriaRecetaQuery
    {
        private readonly RecetasContext _context;

        public CategoriaRecetaQuery(RecetasContext context)
        {
            _context = context;
        }

        public Task<CategoriaReceta> getCategoriaRecetaById(int id)
        {
            return _context.CategoriasReceta.FirstOrDefaultAsync(cr => cr.CategoriaRecetaId == id);
        }
    }
}
