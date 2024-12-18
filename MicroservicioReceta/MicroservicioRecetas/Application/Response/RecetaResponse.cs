namespace Application.Response
{
    public class RecetaResponse
    {
        public Guid Id { get; set; }
        public CategoriaRecetaResponse Categoria { get; set; }
        public Guid UsuarioId { get; set; }
        public DificultadResponse Dificultad { get; set; }
        public string Titulo { get; set; }
        public string FotoReceta { get; set; }
        public string Video { get; set; }
        public string TiempoPreparacion { get; set; }
        public string Topics { get; set; }
        public List<PasoResponse> pasos { get; set; }
        public List<IngredienteRecetaResponse> ingredientes { get; set; }
    }
}
