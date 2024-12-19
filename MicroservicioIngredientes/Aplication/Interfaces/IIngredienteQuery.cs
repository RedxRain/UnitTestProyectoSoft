using Domain.Entities;

namespace Application.Interfaces
{
    public interface IIngredienteQuery
    {
        Ingrediente GetById(int ingredienteId);
        Task<List<Ingrediente>> GetAll();
    }
}
