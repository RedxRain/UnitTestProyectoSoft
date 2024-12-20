using Application.Exceptions;
using Application.Interfaces.Querys;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.Response;

namespace Application.UseCases.SDificultad
{
    public class DificultadService : IDificultadService
    {
        private readonly IDificultadQuery _query;
        DificultadMapper mapper;

        public DificultadService(IDificultadQuery query)
        {
            _query = query;
            mapper = new DificultadMapper();
        }

        public async Task<List<DificultadResponse>> GetListDificultad()
        {
            try
            {
                return await mapper.GetListDificultadResponse(await _query.GetListDificultades());
            }
            catch (BadRequest ex)
            {
                throw new Exceptions.BadRequest("Error: " + ex.Message);
            }

        }

        public async Task<bool> ValidateDificultadById(int dificultadId)
        {
            try
            {
                return (await _query.GetDificultadById(dificultadId) != null);
            }
            catch (Exceptions.BadRequest ex)
            {
                throw new Exceptions.BadRequest(ex.Message);
            }
        }
    }
}
