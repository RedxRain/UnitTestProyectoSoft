using Domain.Entities;

namespace Application.Interfaces.Querys
{
    public interface IDificultadQuery
    {
        public Task<List<Dificultad>> GetListDificultades();
        public Task<Dificultad> GetDificultadById(int dificultadId);
    }
}
