using Application.Interfaces.Mappers;
using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public class RecetaMapper : IRecetaMapper
    {
        private readonly IPasoMapper _pasoMapper;
        private readonly IDificultadMapper _dificultadMapper;
        private readonly IIngredienteRecetaMapper _ingRecMapper;
        private readonly ICategoriaRecetaMapper _catRecMapper;

        public RecetaMapper(IPasoMapper pasoMapper, IDificultadMapper dificultadMapper, IIngredienteRecetaMapper ingredienteRecetaMapper, ICategoriaRecetaMapper categoriaRecetaMapper)
        {
            _pasoMapper = pasoMapper;
            _dificultadMapper = dificultadMapper;
            _ingRecMapper = ingredienteRecetaMapper;
            _catRecMapper = categoriaRecetaMapper;
        }

        public async Task<RecetaResponse> CreateRecetaResponse(Receta unaReceta)
        {
            var receta = new RecetaResponse
            {
                Id = unaReceta.RecetaId,
                Categoria = await _catRecMapper.GetCategoriaRecetaResponse(unaReceta.CategoriaReceta),
                Dificultad = await _dificultadMapper.GetDificultadResponse(unaReceta.Dificultad),
                FotoReceta = unaReceta.FotoReceta,
                TiempoPreparacion = unaReceta.TiempoPreparacion.ToString(),
                Titulo = unaReceta.Titulo,
                Video = unaReceta.Video,
                Topics = unaReceta.Topics,
                pasos = await _pasoMapper.GetListPasosResponse(unaReceta.Pasos),
                ingredientes = await _ingRecMapper.GetIngredientesRecetaResponse(unaReceta.IngredentesReceta)
            };
            return receta;
        }
    }
}
