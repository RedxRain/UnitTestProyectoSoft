using Application.Exceptions;
using Application.Interfaces.Querys;
using Application.Interfaces.Services;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Querys
{
    public class RecetaQuery : IRecetaQuery
    {
        private readonly RecetasContext _context;
        private readonly IUserIngredienteService _userIngredienteService;

        public RecetaQuery(RecetasContext context, IUserIngredienteService userIngredienteService)
        {
            _context = context;
            _userIngredienteService = userIngredienteService;
        }

        public async Task<List<Receta>> GetListRecetas()
        {
            try
            {
                var recetas = await _context.Recetas
                .Include(r => r.Dificultad)
                .Include(r => r.CategoriaReceta)
                .Include(r => r.Pasos)
                .Include(r => r.IngredentesReceta)
                .ToListAsync();

                return recetas;
            }

            catch (DbUpdateException)
            {
                throw new Conflict("Error en la base de datos");
            }
        }

        public async Task<Receta> GetRecetaById(Guid id)
        {
            try
            {
                return await _context.Recetas
                    .Include(r => r.Dificultad)
                    .Include(r => r.CategoriaReceta)
                    .Include(r => r.Pasos)
                    .Include(r => r.IngredentesReceta)
                    //.Include(r => r.UsuarioId)
                    .SingleOrDefaultAsync(r => r.RecetaId == id);
            }
            catch (DbUpdateException)
            {
                throw new BadRequest("Hubo un problema al buscar la receta");
            }
        }

        public async Task<List<Receta>> GetRecetasByFilters(string? tituloIngredienteTopic, int dificultad, int categoria)
        {
            try
            {
                return await _context.Recetas
                .Where(e =>
                (tituloIngredienteTopic != null && e.Titulo.ToLower().Contains(tituloIngredienteTopic.ToLower())) ||
                (dificultad != null && e.Dificultad.DificultadId == dificultad) ||
                (categoria != null && e.CategoriaReceta.CategoriaRecetaId == categoria) ||
                (tituloIngredienteTopic != null && e.Topics.ToLower().Contains(tituloIngredienteTopic.ToLower())))
                .Include(r => r.Dificultad)
                .Include(r => r.IngredentesReceta)
                .Include(r => r.CategoriaReceta)
                .Include(r => r.Pasos)
                .ToListAsync();
            }

            catch (DbUpdateException)
            {
                throw new BadRequest("Hubo un problema al buscar la receta");
            }
        }

        public async Task<List<Receta>> GetRecetasByString(string text)
        {
            try
            {
                throw new NotImplementedException();
            }

            catch (DbUpdateException)
            {
                throw new BadRequest("Hubo un problema al buscar la receta");
            }
        }

        public async Task<int> GetTitleLength()
        {
            try
            {
                return _context.Model.FindEntityType(typeof(Receta))
                   .FindProperty("Titulo")
                   .GetMaxLength().GetValueOrDefault();
            }
            catch (DbUpdateException)
            {
                throw new BadRequest("Hubo un problema al encontrar el limite de la longitud del titulo");
            }

        }
        public async Task<int> GetFotoRecetaLength()
        {
            try
            {
                return _context.Model.FindEntityType(typeof(Receta))
                   .FindProperty("FotoReceta")
                   .GetMaxLength().GetValueOrDefault();
            }
            catch (DbUpdateException)
            {
                throw new BadRequest("Hubo un problema al encontrar el limite de la longitud de la imagen");
            }
        }
        public async Task<int> GetVideoLenght()
        {
            try
            {
                return _context.Model.FindEntityType(typeof(Receta))
                   .FindProperty("Video")
                   .GetMaxLength().GetValueOrDefault();
            }
            catch (DbUpdateException)
            {
                throw new BadRequest("Hubo un problema al encontrar el limite de la longitud del video");
            }
        }
    }
}
