using Application.Exceptions;
using Application.Interfaces.Querys;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Querys
{
    public class IngredienteRecetaQuery : IIngredienteRecetaQuery
    {
        private readonly RecetasContext _context;

        public IngredienteRecetaQuery(RecetasContext context)
        {
            _context = context;
        }

        public async Task<IngredienteReceta> GetIngRecetaById(int id)
        {
            try
            {
                return await _context.IngredientesRecetas
                        .SingleOrDefaultAsync(ir => ir.IngredienteId == id);
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un error en la búsqueda del ingrediente receta");
            }
        }
        public async Task<bool> ExistIngredienteInIngReceta(Guid recetaId, int ingredienteId)
        {
            try
            {
                return (await _context.IngredientesRecetas
                .SingleOrDefaultAsync(ir => ir.RecetaId == recetaId && ir.IngredienteId == ingredienteId) != null);
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un error en la búsqueda del ingrediente receta");
            }
        }
        public async Task<Guid> GetRecetaIdByIngRecetaId(int ingRecetaId)
        {
            try
            {
                return (await _context.IngredientesRecetas
                    .SingleOrDefaultAsync(ir => ir.IngredienteRecetaId == ingRecetaId)).RecetaId;
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un error en la búsqueda del ingrediente receta");
            }

        }

        public async Task<int> GetIngRecetaByRecetaId(Guid recetaId, int ingredienteId)
        {
            try
            {
                return (await _context.IngredientesRecetas
                    .SingleOrDefaultAsync(ir => ir.RecetaId.Equals(recetaId) && ir.IngredienteId == ingredienteId)).IngredienteRecetaId;
            }
            catch (DbUpdateException)
            {
                throw new BadRequestt("Hubo un error en la búsqueda del ingrediente receta");
            }
        }
    }
}
