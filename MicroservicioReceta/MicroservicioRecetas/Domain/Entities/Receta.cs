namespace Domain.Entities
{
    public class Receta
    {
        public Guid RecetaId { get; set; }
        public int CategoriaRecetaId { get; set; }
        public CategoriaReceta CategoriaReceta { get; set; }
        public int DificultadId { get; set; }
        public Dificultad Dificultad { get; set; }
        public Guid UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string FotoReceta { get; set; }
        public string Video { get; set; }
        public string Topics { get; set; }
        public TimeSpan TiempoPreparacion { get; set; }

        public ICollection<Paso> Pasos { get; set; }
        public ICollection<IngredienteReceta> IngredentesReceta { get; set; }
        public PromedioPuntaje PromedioPuntaje { get; set; }

    }
}
