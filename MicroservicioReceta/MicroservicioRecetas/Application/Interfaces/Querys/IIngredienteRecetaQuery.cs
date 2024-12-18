using Domain.Entities;

namespace Application.Interfaces.Querys
{
    public interface IIngredienteRecetaQuery
    {
        Task<IngredienteReceta> GetIngRecetaById(int id);
        Task<bool> ExistIngredienteInIngReceta(Guid recetaId, int ingredienteId);
        Task<Guid> GetRecetaIdByIngRecetaId(int ingRecetaId);
        Task<int> GetIngRecetaByRecetaId(Guid recetaId, int ingredienteId);
    }
}
