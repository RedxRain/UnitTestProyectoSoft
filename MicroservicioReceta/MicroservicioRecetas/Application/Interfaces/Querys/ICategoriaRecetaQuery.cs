using Domain.Entities;

namespace Application.Interfaces.Querys
{
    public interface ICategoriaRecetaQuery
    {
        Task<CategoriaReceta> getCategoriaRecetaById(int id);
    }
}
