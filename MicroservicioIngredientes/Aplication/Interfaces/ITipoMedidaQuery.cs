using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITipoMedidaQuery
    {
        TipoMedida GetTipoMedida(int tipoMedidaId);
        List<TipoMedida> GetListTipoMedida();
    }
}
