using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITipoIngredienteQuery
    {
        TipoIngrediente GetById(int tipoIngredienteId);
        List<TipoIngrediente> GetAll();
    }
}
