using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IIngredienteRecetaMapper
    {
        Task<IngredienteRecetaResponse> GetIngredienteRecetaResponse(IngredienteReceta unIngRec);
        Task<List<IngredienteRecetaResponse>> GetIngredientesRecetaResponse(ICollection<IngredienteReceta> listaIngReceta);
        Task<IngredienteRecetaDeleteResponse> GetIngredienteDeleteResponse(IngredienteReceta unIngRec);
    }
}
