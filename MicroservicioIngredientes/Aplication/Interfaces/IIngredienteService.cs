using Application.Models;
using Application.Response;

namespace Application.Interfaces
{
    public interface IIngredienteService
    {
        Task<IngredienteResponse> CreateIngrediente(IngredienteRequest request);
        Task<IngredienteResponse> GetById(int id);
        Task<List<IngredienteResponse>> GetByName(string name);
    }
}
