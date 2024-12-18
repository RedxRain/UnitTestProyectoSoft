using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IRecetaMapper
    {
        Task<RecetaResponse> CreateRecetaResponse(Receta unaReceta);
    }
}