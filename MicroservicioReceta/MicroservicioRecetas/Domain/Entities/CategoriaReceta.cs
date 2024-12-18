namespace Domain.Entities
{
    public class CategoriaReceta
    {
        public int CategoriaRecetaId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Receta> Recetas { get; set; }
    }
}
