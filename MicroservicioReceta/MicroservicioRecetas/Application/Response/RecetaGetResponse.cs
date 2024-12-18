namespace Application.Response
{
    public class RecetaGetResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public CategoriaRecetaResponse Categoria { get; set; }
        public Guid UsuarioId { get; set; }
        public DificultadResponse Dificultad { get; set; }
        public string FotoReceta { get; set; }
    }
}
