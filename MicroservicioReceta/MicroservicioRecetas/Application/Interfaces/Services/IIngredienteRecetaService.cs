using Application.Request;
using Application.Response;

namespace Application.Interfaces.Services
{
    public interface IIngredienteRecetaService
    {
        Task<IngredienteRecetaResponse> CreateIngredienteReceta(IngredienteRecetaRequest request, Guid recetaId);
        //Task<IngredienteRecetaResponse> UpdateIngredienteReceta(IngredienteRecetaRequest request, Guid recetaId);
        Task<bool> DeleteAllIngRecetaWhitRecetaId(Guid recetaId);
        Task<IngredienteRecetaDeleteResponse> DeleteIngredienteReceta(int ingRecetaId);
        Task<int> GetIngredienteRecetaBy(Guid recetaId, int ingRecetaId);
    }
}
