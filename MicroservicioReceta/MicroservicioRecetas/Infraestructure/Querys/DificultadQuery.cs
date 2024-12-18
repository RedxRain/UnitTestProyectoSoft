using Application.Exceptions;
using Application.Interfaces.Querys;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Infraestructure.Querys
{
    public class DificultadQuery : IDificultadQuery
    {
        private readonly RecetasContext _context;

        public DificultadQuery(RecetasContext recetasContext)
        {
            _context = recetasContext;
        }

        public async Task<List<Dificultad>> GetListDificultades()
        {
            try
            {
                return await _context.Dificultades.ToListAsync();
            }
            catch (DbException ex)
            {
                throw new BadRequestt("Hubo un problema en la búsqueda de listas de dificultades");
            }
        }

        public async Task<Dificultad> GetDificultadById(int dificultadId)
        {
            try
            {
                return await _context.Dificultades
                    .SingleOrDefaultAsync(d => d.DificultadId == dificultadId);
            }
            catch (DbException ex)
            {
                throw new BadRequestt("Hubo un error en la búsqueda de la dificultad");
            }
        }
    }
}
