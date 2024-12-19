using Domain.Entities;

namespace Application.Interfaces
{
    public interface IIngredienteCommand
    {
        Task<Ingrediente> Insert(Ingrediente ingrediente);
        Task<Ingrediente> Remove(int ingredienteId);
        Task<Ingrediente> Update(Ingrediente ingrediente, Ingrediente ingredienteUpdate);
    }
}
