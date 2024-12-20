using Application.Request;

namespace UniTestReceta.ClasesUtiles
{
    internal class ReturnRecetaRequest
    {
        public RecetaRequest RetornarRecetaRequest(int categoriaRecetaId, int dificultadId, string titulo, string video, string foto, string horario)
        {
            return new RecetaRequest
            {
                CategoriaRecetaId = categoriaRecetaId,
                DificultadId = dificultadId,
                UsuarioId = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F9"),
                Titulo = titulo,
                FotoReceta = foto,
                Video = video,
                TiempoPreparacion = horario,
                Topics = "casero",
                ListaPasos = new List<PasoRequest> {
                    (new PasoRequest
                    {
                        Orden = 1,
                        Descripcion = "Descripcion",
                        Foto = "Foto"
                    }) },
                ListaIngredienteReceta = new List<IngredienteRecetaRequest> {
                    new IngredienteRecetaRequest
                    {
                        ingredienteId = 1,
                        cantidad = 1
                    }
                }
            };
        }
    }
}
