using Application.Interfaces.Mappers;
using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public class RecetaGetResponseMapper : IRecetaGetResponseMapper
    {
        private readonly IDificultadMapper _dificultadMapper;
        private readonly ICategoriaRecetaMapper _catRecMapper;

        public RecetaGetResponseMapper(IDificultadMapper dificultadMapper, ICategoriaRecetaMapper categoriaRecetaMapper)
        {
            _dificultadMapper = dificultadMapper;
            _catRecMapper = categoriaRecetaMapper;
        }
        public async Task<RecetaGetResponse> CreateRecetaGetResponse(Receta unaRecetaGetResponse)
        {
            var recetaGetResponse = new RecetaGetResponse
            {
                Titulo = unaRecetaGetResponse.Titulo,
                Id = unaRecetaGetResponse.RecetaId,
                Categoria = await _catRecMapper.GetCategoriaRecetaResponse(unaRecetaGetResponse.CategoriaReceta),
                Dificultad = await _dificultadMapper.GetDificultadResponse(unaRecetaGetResponse.Dificultad),
                FotoReceta = unaRecetaGetResponse.FotoReceta,
                //Usuario
            };
            return recetaGetResponse;
        }
    }
}
