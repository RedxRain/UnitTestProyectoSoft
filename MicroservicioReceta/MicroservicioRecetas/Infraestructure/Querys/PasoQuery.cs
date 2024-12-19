using Application.Exceptions;
using Application.Interfaces.Querys;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Querys
{
    public class PasoQuery : IPasoQuery
    {
        private readonly RecetasContext _context;

        public PasoQuery(RecetasContext context)
        {
            _context = context;
        }


        public async Task<List<Paso>> GetPasosByRecetaId(Guid recetaId)
        {
            try
            {
                List<Paso> results = await _context.Pasos
                .Where(p => p.RecetaId == recetaId)
                .ToListAsync();
                return results;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }
        public async Task<Paso> GetPasoById(int pasoId)
        {
            try
            {
                var paso = await _context.Pasos
                    .SingleOrDefaultAsync(p => p.PasoId == pasoId);
                return paso;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionNotFound("No se encontró el paso solicitado");
            }
        }

        public async Task<int> GetDescripcionLength()
        {
            try
            {
                return _context.Model.FindEntityType(typeof(Paso))
                   .FindProperty("Descripcion")
                   .GetMaxLength().GetValueOrDefault();
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un problema al encontrar el limite de la longitud de la descripcion");
            }
        }

        public async Task<int> GetFotoLength()
        {
            try
            {
                return _context.Model.FindEntityType(typeof(Paso))
                   .FindProperty("Foto")
                   .GetMaxLength().GetValueOrDefault();
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un problema al encontrar el limite de la longitud de la foto");
            }
        }

        public async Task<int> GetPasoIdByRecetaId(Guid recetaId, int orden)
        {
            try
            {
                int paso = _context.Pasos
                    .SingleOrDefaultAsync(p => p.RecetaId == recetaId && p.Orden == orden).Result.PasoId;
                return paso;
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un problema al encontrar el limite de la longitud de la foto");
            }
        }
    }
}
