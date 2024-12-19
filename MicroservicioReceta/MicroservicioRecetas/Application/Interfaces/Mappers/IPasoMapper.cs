using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IPasoMapper
    {
        Task<PasoResponse> GetPasoResponse(Paso unPaso);
        Task<List<PasoResponse>> GetListPasosResponse(ICollection<Paso> listaPasos);
    }
}
