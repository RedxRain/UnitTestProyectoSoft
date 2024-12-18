using Application.Interfaces.Mappers;
using Application.Response;
using Domain.Entities;

namespace Application.Mappers
{
    public class DificultadMapper : IDificultadMapper
    {
        public async Task<DificultadResponse> GetDificultadResponse(Dificultad dificultad)
        {
            return new DificultadResponse
            {
                Id = dificultad.DificultadId,
                Nombre = dificultad.Nombre
            };
        }
        public async Task<List<DificultadResponse>> GetListDificultadResponse(List<Dificultad> dificutades)
        {
            List<DificultadResponse> dificultadesResponse = new();
            foreach (Dificultad dificultad in dificutades)
            {
                dificultadesResponse.Add(await GetDificultadResponse(dificultad));
            }
            return dificultadesResponse;
        }
    }


}
