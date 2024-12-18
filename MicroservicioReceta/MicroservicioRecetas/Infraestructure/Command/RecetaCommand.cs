using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Request;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class RecetaCommand : IRecetaCommand
    {
        private readonly RecetasContext _context;

        public RecetaCommand(RecetasContext context)
        {
            _context = context;
        }

        public async Task<Receta> CreateReceta(Receta receta)
        {
            try
            {
                _context.Add(receta);
                return receta;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }

        public async Task<Receta> DeleteReceta(Receta unaReceta)
        {
            try
            {
                _context.Remove(unaReceta);
                return unaReceta;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("No se pudo eliminar la receta");
            }
        }

        public async Task<Receta> UpdateReceta(RecetaRequest recetaRequest, Guid recetaId)
        {
            try
            {
                var recetaToUpdate = await _context.Recetas.FirstOrDefaultAsync(r => r.RecetaId.Equals(recetaId));
                recetaToUpdate.Titulo = recetaRequest.Titulo;
                recetaToUpdate.DificultadId = recetaRequest.DificultadId;
                recetaToUpdate.FotoReceta = recetaRequest.FotoReceta;
                recetaToUpdate.Video = recetaRequest.Video;

                return recetaToUpdate;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() == 2;
        }
    }
}
