using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITipoMedidaCommand
    {
        Task<TipoMedida> Insert(TipoMedida tipoMedida);
        Task<TipoMedida> Remove(int tipoMedidaId);
    }
}
