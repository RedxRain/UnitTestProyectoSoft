using Application.Response;

namespace Application.Interfaces
{
    public interface ITipoIngredienteService
    {
        Task<TipoIngredienteResponse> GetByTipoIngrediente(int id);
    }
}
