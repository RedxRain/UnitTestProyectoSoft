using Application.Interfaces.Mappers;
using Application.Response;
using Domain.Entities;
using Moq;

namespace UniTestReceta.Mocks
{
    internal class MockRecetaMapper
    {
        public IRecetaMapper ConfigureMockRecetaMapper()
        {
            var mockRecetaMapper = new Mock<IRecetaMapper>();

            mockRecetaMapper.Setup(m => m.CreateRecetaResponse(It.IsAny<Receta>())).ReturnsAsync((Receta r) =>
            {
                return new RecetaResponse
                {
                    Id = r.RecetaId,
                    Categoria = new CategoriaRecetaResponse
                    {
                        Id = 1,
                        Nombre = "Pasta"
                    },
                    UsuarioId = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F9"),
                    Dificultad = new DificultadResponse
                    {
                        Id = 1,
                        Nombre = "Facil" 
                    },
                    Titulo = r.Titulo,
                    FotoReceta = r.FotoReceta,
                    Video = r.Video,
                    TiempoPreparacion = r.TiempoPreparacion.ToString(),
                    Topics = r.Topics,
                    Pasos = new Mock<List<PasoResponse>>().Object,
                    Ingredientes = new Mock<List<IngredienteRecetaResponse>>().Object
                };
            });
            return mockRecetaMapper.Object;
        }
    }
}
