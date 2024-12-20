using Application.Interfaces.Commands;
using Application.Request;
using Domain.Entities;
using Moq;
using UniTestReceta.ClasesUtiles;

namespace UniTestReceta.Mocks
{
    internal class MockRecetaCommand
    {
        public IRecetaCommand ConfigureMockRecetaCommand()
        {
            var returnReceta = new ReturnReceta();
            var mockCommand = new Mock<IRecetaCommand>();
            var receta = returnReceta.RetornarReceta();
            mockCommand.Setup(c => c.DeleteReceta(It.IsAny<Receta>())).ReturnsAsync((Receta r) =>
            {
                if (r.RecetaId != receta.RecetaId || r == null)
                    return null;

                return receta;
            });

            mockCommand.Setup(c => c.CreateReceta(It.IsAny<Receta>())).ReturnsAsync((Receta r) =>
            {
                return r;
            });

            mockCommand.Setup(c => c.UpdateReceta(It.IsAny<RecetaRequest>(), It.IsAny<Guid>())).ReturnsAsync((RecetaRequest r, Guid id) =>
            {
                var mockListReceta = new Mock<List<Receta>>().Object;
                return new Receta
                {
                    RecetaId = id,
                    CategoriaRecetaId = r.CategoriaRecetaId,
                    CategoriaReceta = new CategoriaReceta
                    {
                        CategoriaRecetaId = r.CategoriaRecetaId,
                        Nombre = "Pasta",
                        Recetas = mockListReceta
                    },
                    DificultadId = r.DificultadId,
                    Dificultad = new Dificultad
                    {
                        DificultadId = r.DificultadId,
                        Nombre = "Facil",
                        Recetas = mockListReceta
                    },
                    UsuarioId = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F9"),
                    Titulo = r.Titulo,
                    FotoReceta = r.FotoReceta,
                    Video = r.Video,
                    Topics = r.Topics,
                    TiempoPreparacion = new TimeSpan(03, 30, 0),
                    Pasos = new Mock<List<Paso>>().Object,
                    IngredentesReceta = new Mock<List<IngredienteReceta>>().Object,
                    PromedioPuntaje = new PromedioPuntaje
                    {
                        PromedioPuntajeId = 1,
                        RecetaId = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8"),
                        Receta = (new Mock<Receta>()).Object
                    }
                };
            });

            return mockCommand.Object;
        }
    }
}
