using Application.Interfaces.Mappers;
using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public class PasoMapper : IPasoMapper
    {
        public Task<PasoResponse> GetPasoResponse(Paso unPaso)
        {
            return Task.FromResult(new PasoResponse
            {
                Id = unPaso.PasoId,
                Descripcion = unPaso.Descripcion,
                Foto = unPaso.Foto,
                Orden = unPaso.Orden
            });
        }
        public Task<List<PasoResponse>> GetListPasosResponse(ICollection<Paso> listaPasos)
        {
            List<PasoResponse> pasosResponse = new();
            foreach (Paso unPaso in listaPasos)
            {
                pasosResponse.Add(new PasoResponse
                {
                    Id = unPaso.PasoId,
                    Orden = unPaso.Orden,
                    Descripcion = unPaso.Descripcion,
                    Foto = unPaso.Foto,
                });
            }
            return Task.FromResult(pasosResponse);
        }

    }
}
