using Domain.Entities;
using Moq;

namespace UniTestReceta.ClasesUtiles
{
    internal class ReturnReceta
    {
        public Receta RetornarReceta()
        {
            var mockListReceta = new Mock<List<Receta>>().Object;

            Receta receta = new Receta
            {
                RecetaId = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8"),
                CategoriaRecetaId = 1,
                CategoriaReceta = new CategoriaReceta
                {
                    CategoriaRecetaId = 1,
                    Nombre = "Pasta",
                    Recetas = mockListReceta
                },
                DificultadId = 1,
                Dificultad = new Dificultad
                {
                    DificultadId = 1,
                    Nombre = "Facil",
                    Recetas = mockListReceta
                },
                UsuarioId = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F9"),
                Titulo = "Ñoquis",
                FotoReceta = "Foto",
                Video = "Video",
                Topics = "Casero",
                TiempoPreparacion = new TimeSpan(2, 30, 0),
                Pasos = new Mock<List<Paso>>().Object,
                IngredentesReceta = new Mock<List<IngredienteReceta>>().Object,
                PromedioPuntaje = new PromedioPuntaje
                {
                    PromedioPuntajeId = 1,
                    RecetaId = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8"),
                    Receta = (new Mock<Receta>()).Object
                }
            };

            return receta;
        }
    }
}
