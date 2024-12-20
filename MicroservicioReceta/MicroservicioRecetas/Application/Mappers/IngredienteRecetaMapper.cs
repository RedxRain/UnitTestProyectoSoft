using Application.Interfaces.Mappers;
using Application.Interfaces.Services;
using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public class IngredienteRecetaMapper : IIngredienteRecetaMapper
    {
        private readonly IUserIngredienteService _userIngredienteService;

        public IngredienteRecetaMapper(IUserIngredienteService userIngredienteService)
        {
            _userIngredienteService = userIngredienteService;
        }

        public async Task<IngredienteRecetaResponse> GetIngredienteRecetaResponse(IngredienteReceta unIngRec)
        {
            return new IngredienteRecetaResponse
            {
                id = unIngRec.IngredienteRecetaId,
                nombre = _userIngredienteService.GetIngredienteName(unIngRec.IngredienteId),
                //ingredienteId = 12,
            };

        }

        public async Task<List<IngredienteRecetaResponse>> GetIngredientesRecetaResponse(ICollection<IngredienteReceta> listaIngReceta)
        {
            List<IngredienteRecetaResponse> ingRecetasResponse = new();
            foreach (IngredienteReceta ingReceta in listaIngReceta)
            {
                ingRecetasResponse.Add(await GetIngredienteRecetaResponse(ingReceta));
            }
            return ingRecetasResponse;
        }

        public async Task<IngredienteRecetaDeleteResponse> GetIngredienteDeleteResponse(IngredienteReceta unIngRec)
        {
            return (new IngredienteRecetaDeleteResponse
            {
                id = unIngRec.IngredienteRecetaId,
                //Tiene que traer el nombre desde el microservicio ingrediente
                nombre = "sarasa",
            });
        }
    }
}
