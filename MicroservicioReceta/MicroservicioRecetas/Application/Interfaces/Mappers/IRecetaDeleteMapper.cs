using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IRecetaDeleteMapper
    {
        Task<RecetaDeleteResponse> CreateRecetaDeleteResponse(Receta recetaToDelete);
    }
}
