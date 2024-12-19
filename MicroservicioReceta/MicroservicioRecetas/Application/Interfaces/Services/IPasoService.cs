using Application.Request;
using Application.Response;

namespace Application.Interfaces.Services
{
    public interface IPasoService
    {
        Task<PasoResponse> CreatePaso(PasoRequest request, Guid recetaId);
        Task<PasoResponse> UpdatePaso(PasoRequest request, int id);
        Task<PasoResponse> DeletePaso(int id);

        Task<bool> DeleteAllPasosWhitRecetaId(Guid recetaId);
        Task<List<PasoResponse>> GetPasosByRecetaId(Guid recetaId);
        Task<PasoResponse> GetPasoById(int Id);
        Task<int> GetPasoidByRecetaId(Guid recetaId, int paso);

    }
}
