namespace Application.Request
{
    public class RecetaRequest
    {

        public int CategoriaRecetaId { get; set; }
        public int DificultadId { get; set; }
        public Guid UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string FotoReceta { get; set; }
        public string Video { get; set; }
        public string TiempoPreparacion { get; set; }
        public string Topics { get; set; }
        public List<PasoRequest> ListaPasos { get; set; }
        public List<IngredienteRecetaRequest> ListaIngredienteReceta { get; set; }

    }
}
