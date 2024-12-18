namespace Domain.Entities
{
    public class TipoMedida
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Ingrediente> Ingredientes { get; set; }
    }
}
