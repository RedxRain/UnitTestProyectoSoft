using Application.Interfaces.Querys;
using Application.Interfaces.Services;

namespace Application.UseCases.CategoriaReceta
{
    public class CategoriaRecetaService : ICategoriaRecetaService
    {
        private readonly ICategoriaRecetaQuery _categoriaQuery;

        public CategoriaRecetaService(ICategoriaRecetaQuery categoriaQuery)
        {
            _categoriaQuery = categoriaQuery;
        }

        public async Task<bool> ValidateCategoriaRecetaById(int categoriaRecetaId)
        {
            return !(await _categoriaQuery.getCategoriaRecetaById(categoriaRecetaId) == null);
        }
    }
}
