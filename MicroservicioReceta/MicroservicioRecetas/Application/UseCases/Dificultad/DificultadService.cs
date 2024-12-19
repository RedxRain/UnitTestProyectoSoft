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
            catch (BadRequestt ex)
            {
                throw new Exceptions.BadRequestt("Error: " + ex.Message);
            }

        }

        //public async Task<DificultadResponse> GetDificultadById(int id)
        //{
        //    try
        //    {
        //        return await mapper.GetDificultadResponse(await _query.GetDificultadById(id));
        //    }
        //    catch (ExceptionNotFound)
        //    {
        //        throw new ExceptionNotFound("No existe esa dificultad");
        //    }
        //}

        public async Task<bool> ValidateDificultadById(int dificultadId)
        {
            try
            {
                return (await _query.GetDificultadById(dificultadId) != null);
            }
            catch (Exceptions.BadRequestt ex)
            {
                throw new Exceptions.BadRequestt(ex.Message);
            }
        }
    }
}
