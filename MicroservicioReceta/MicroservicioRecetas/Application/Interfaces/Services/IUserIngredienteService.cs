namespace Application.Interfaces.Services
{
    public interface IUserIngredienteService
    {
        dynamic GetByID(int Id);
        dynamic GetByName(string Name);
        string GetIngredienteName(int ingredienteId);
    }
}
