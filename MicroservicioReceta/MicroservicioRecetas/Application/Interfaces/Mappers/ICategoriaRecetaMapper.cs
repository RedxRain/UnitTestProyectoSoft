using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface ICategoriaRecetaMapper
    {
        Task<CategoriaRecetaResponse> GetCategoriaRecetaResponse(CategoriaReceta categoriaReceta);
    }
}
