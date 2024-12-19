using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITipoIngredienteCommand
    {
        Task<TipoIngrediente> Insert(TipoIngrediente tipoIngrediente);
        Task<TipoIngrediente> Remove(int tipoIngredienteId);
        Task<TipoIngrediente> Update(TipoIngrediente tipoIngrediente);
    }
}
