using Application.Interfaces.Mappers;
using Application.Response;
using Domain.Entities;
using Moq;

namespace UniTestReceta.Mocks
{
    internal class MockRecetaDeleteMapper
    {
        public IRecetaDeleteMapper ConfigureMockRecetaDeleteMapper()
        {
            var mockRecetaDeleteMapper = new Mock<IRecetaDeleteMapper>();

            mockRecetaDeleteMapper.Setup(m => m.CreateRecetaDeleteResponse(It.IsAny<Receta>())).ReturnsAsync(() =>
            {
                return new RecetaDeleteResponse
                {
                    id = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8"),
                    titulo = "Ñoquis"
                };
            });

            return mockRecetaDeleteMapper.Object;
        }
    }
}
