using Application.Exceptions;
using Application.Interfaces;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class TipoIngredienteService : ITipoIngredienteService
    {
        private readonly ITipoIngredienteQuery _query;

        public TipoIngredienteService(ITipoIngredienteQuery query)
        {
            _query = query;
        }

        public async Task<TipoIngredienteResponse> GetByTipoIngrediente(int id)
        {
            var tipoIngre = _query.GetById(id);

            if (tipoIngre == null) { throw new NotFoundException("No existe un Tipo de Ingrediente que contengam ese Id."); }

            return await Task.FromResult(MapearTipoIngrediente(tipoIngre));
        }

        public TipoIngredienteResponse MapearTipoIngrediente(TipoIngrediente tipoIngre)
        {
            return new TipoIngredienteResponse
            {
                Id = tipoIngre.Id,
                Name = tipoIngre.Name,
                List = tipoIngre.Ingredientes.Select(e => new IngredienteGetResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToList()
            };
        }
    }
}
