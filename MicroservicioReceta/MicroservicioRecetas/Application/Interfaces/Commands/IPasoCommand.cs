using Application.Request;
using Domain.Entities;

namespace Application.Interfaces.Commands
{
    public interface IPasoCommand
    {
        Task<Paso> CreatePaso(Paso paso);
        Task<Paso> UpdatePaso(PasoRequest pasoRequest, int pasoId);
        Task<Paso> DeletePaso(Paso unPaso);
        Task<bool> DeleteAllPasosByRecetaId(Guid recetaId);
    }
}
