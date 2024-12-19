namespace Domain.Entities
{
    public class TipoIngrediente
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Ingrediente> Ingredientes { get; set; }
    }
}
