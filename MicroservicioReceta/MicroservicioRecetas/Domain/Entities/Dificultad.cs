namespace Domain.Entities
{
    public class Dificultad
    {
        public int DificultadId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Receta> Recetas { get; set; }
    }
}
