using Application.Interfaces.Querys;
using Domain.Entities;
using Moq;
using UniTestReceta.ClasesUtiles;

namespace UniTestReceta.Mocks
{
    internal class MockRecetaQuery
    {
        public IRecetaQuery ConfigureMockRecetaQuery()
        {
            var returnReceta = new ReturnReceta();
            var mockQuery = new Mock<IRecetaQuery>();
            var receta = returnReceta.RetornarReceta();
            mockQuery.Setup(q => q.GetRecetaById(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                if (id != receta.RecetaId || id == null)
                    return null;

                return receta;
            });

            mockQuery.Setup(q => q.GetListRecetas()).ReturnsAsync(() =>
            { return new List<Receta>() { receta }; }
            );

            mockQuery.Setup(q => q.GetRecetasByFilters(It.IsAny<String>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((string tituloIngredienteTopic, int dificultad, int categoria) =>
                {
                    if ((tituloIngredienteTopic != null && receta.Titulo.ToLower().Contains(tituloIngredienteTopic.ToLower())) ||
                (dificultad != null && receta.Dificultad.DificultadId == dificultad) ||
                (categoria != null && receta.CategoriaReceta.CategoriaRecetaId == categoria) ||
                (tituloIngredienteTopic != null && receta.Topics.ToLower().Contains(tituloIngredienteTopic.ToLower())))
                    {
                        return new List<Receta>() { receta };
                    }
                    return new List<Receta>();
                });

            mockQuery.Setup(q => q.GetTitleLength()).ReturnsAsync(() => { return 10; });
            mockQuery.Setup(q => q.GetVideoLenght()).ReturnsAsync(() => { return 10; });
            mockQuery.Setup(q => q.GetFotoRecetaLength()).ReturnsAsync(() => { return 10; });

            return mockQuery.Object;
        }
    }
}
