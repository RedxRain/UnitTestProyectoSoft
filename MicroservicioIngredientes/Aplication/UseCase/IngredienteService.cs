using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class IngredienteService : IIngredienteService
    {
        private readonly IIngredienteCommand _command;
        private readonly IIngredienteQuery _query;

        public IngredienteService(IIngredienteCommand command, IIngredienteQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<IngredienteResponse> CreateIngrediente(IngredienteRequest request)
        {
            var ingre = new Ingrediente
            {
                Id=request.Id,
                TipoIngredienteID = request.TipoIngredienteID,
                TipoMedidaID = request.TipoMedidaID,
                Name = request.Nombre,
            };

            var result = await _command.Insert(ingre);

            return await Task.FromResult(MapearIngrediente(result));
        }

        public async Task<IngredienteResponse> GetById(int id)
        {
            var ingre = _query.GetById(id);

            if (ingre == null) { throw new NotFoundException("No existe Ingrediente con ese Id."); }

            return await Task.FromResult(MapearIngrediente(ingre));
        }

        public async Task<List<IngredienteResponse>> GetByName(string name)
        {
                var listIngre = (await _query.GetAll()).ToList()
                .Where(e => (name != null && e.Name.Contains(name)))
                .Select(e => MapearIngrediente(e));

                if (!listIngre.Any()) { throw new NotFoundException("No existen Ingredientes con ese nombre."); }

                return await Task.FromResult(listIngre.ToList());
        }

        private IngredienteResponse MapearIngrediente(Ingrediente ingre)
        {
            return new IngredienteResponse
            {
                Id = ingre.Id,
                Name = ingre.Name,
                TipoIngrediente = new TipoIngredienteGetResponse
                {
                    Id = ingre.TipoIngrediente.Id,
                    Name = ingre.TipoIngrediente.Name,
                },
                TipoMedida = new TipoMedidaGetResponse
                {
                    Id = ingre.TipoMedida.Id,
                    Name = ingre.TipoMedida.Name
                }
            };
        }
    }
}
