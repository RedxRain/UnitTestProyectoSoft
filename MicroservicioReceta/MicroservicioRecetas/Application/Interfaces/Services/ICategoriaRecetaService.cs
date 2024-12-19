namespace Application.Interfaces.Services
{
    public interface ICategoriaRecetaService
    {
        Task<bool> ValidateCategoriaRecetaById(int categoriaRecetaId);
    }
}
