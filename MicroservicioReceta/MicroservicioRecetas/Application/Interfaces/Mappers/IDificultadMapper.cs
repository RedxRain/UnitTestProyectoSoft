using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IDificultadMapper
    {
        Task<DificultadResponse> GetDificultadResponse(Dificultad dificultad);
        Task<List<DificultadResponse>> GetListDificultadResponse(List<Dificultad> dificutades);
    }
}
