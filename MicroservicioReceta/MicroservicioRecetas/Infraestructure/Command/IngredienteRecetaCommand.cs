using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Request;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class IngredienteRecetaCommand : IIngredienteRecetaCommand
    {
        private readonly RecetasContext _context;

        public IngredienteRecetaCommand(RecetasContext context)
        {
            _context = context;
        }
        public async Task<IngredienteReceta> CreateIngredienteReceta(IngredienteReceta ingReceta)
        {
            try
            {
                _context.Add(ingReceta);
                //await _context.SaveChangesAsync();
                return ingReceta;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("No se pudo eliminar la receta");
            }
        }

        public async Task<IngredienteReceta> DeleteIngredienteReceta(IngredienteReceta ingReceta)
        {
            try
            {
                _context.Remove(ingReceta);
                await _context.SaveChangesAsync();
                return ingReceta;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("No se pudo eliminar la receta");
            }
        }

        public async Task<bool> DeleteAllIngRecetaByRecetaId(Guid recetaId)
        {
            try
            {
                var ingredientesToDelete = _context.IngredientesRecetas
                    .Where(ir => ir.RecetaId.Equals(recetaId));

                _context.IngredientesRecetas.RemoveRange(ingredientesToDelete);
                return await _context.SaveChangesAsync() == 2;
            }
            catch (DbUpdateException)
            {
                throw new Conflict("No se pudo eliminar la receta");
            }
        }

        public async Task<IngredienteReceta> UpdateIngredienteReceta(IngredienteRecetaRequest ingRecetaUpdate, int ingRecetaId, Guid recetaId)
        {
            var ingRecetaToUpdate = await _context.IngredientesRecetas.FirstOrDefaultAsync(ir => ir.IngredienteRecetaId == ingRecetaId);

            ingRecetaToUpdate.cantidad = ingRecetaUpdate.cantidad;
            ingRecetaToUpdate.IngredienteId = ingRecetaUpdate.ingredienteId;
            ingRecetaToUpdate.RecetaId = recetaId;
            await _context.SaveChangesAsync();
            return ingRecetaToUpdate;
        }
    }
}
