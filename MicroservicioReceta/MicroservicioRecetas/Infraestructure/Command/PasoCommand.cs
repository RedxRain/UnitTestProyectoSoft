using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Request;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class PasoCommand : IPasoCommand
    {
        private readonly RecetasContext _context;

        public PasoCommand(RecetasContext context)
        {
            _context = context;
        }

        public async Task<Paso> CreatePaso(Paso paso)
        {
            try
            {
                _context.Add(paso);
                //await _context.SaveChangesAsync();
                return paso;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }

        }

        public async Task<bool> DeleteAllPasosByRecetaId(Guid recetaId)
        {
            try
            {
                var pasosToDelete = _context.Pasos
                    .Where(p => p.RecetaId.Equals(recetaId));

                _context.Pasos.RemoveRange(pasosToDelete);
                return await _context.SaveChangesAsync() == 2;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("No se pudo eliminar la receta");
            }
        }

        public async Task<Paso> DeletePaso(Paso unPaso)
        {
            try
            {
                _context.Remove(unPaso);
                await _context.SaveChangesAsync();
                return unPaso;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("No se pudo eliminar el paso");
            }
        }

        public async Task<Paso> UpdatePaso(PasoRequest pasoRequest, int pasoId)
        {
            try
            {
                //El receta ID no se debería de modificar porque se supone que modificas un paso de una receta
                //Hay que ver en caso de que se modifique una sola cosa, para que todo lo demás no se modifique
                //Así por encima se me ocurre que todo lo que entre con los valores por default los filtre o algo
                var pasoToUpdate = await _context.Pasos.FirstOrDefaultAsync(p => p.PasoId == pasoId);
                pasoToUpdate.Orden = pasoRequest.Orden;
                pasoToUpdate.Descripcion = pasoRequest.Descripcion;
                pasoToUpdate.Foto = pasoRequest.Foto;
                await _context.SaveChangesAsync();

                return pasoToUpdate;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }
    }
}
