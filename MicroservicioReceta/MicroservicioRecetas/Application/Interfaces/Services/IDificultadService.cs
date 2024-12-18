using Application.Response;

namespace Application.Interfaces.Services
{
    public interface IDificultadService
    {
        Task<List<DificultadResponse>> GetListDificultad();
        //Task<DificultadResponse> GetDificultadById(int id);
        Task<bool> ValidateDificultadById(int dificultadId);
    }
}
