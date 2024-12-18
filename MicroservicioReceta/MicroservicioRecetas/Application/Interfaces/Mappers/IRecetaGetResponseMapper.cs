using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IRecetaGetResponseMapper
    {
        Task<RecetaGetResponse> CreateRecetaGetResponse(Receta unaRecetaGetResponse);
    }
}
