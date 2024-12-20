using Application.Interfaces.Services;
using Moq;

namespace UniTestReceta.Mocks
{
    internal class MockCategoriaReceta
    {
        public ICategoriaRecetaService ConfigureMockCategoriaReceta()
        {
            var mockCategoriaReceta = new Mock<ICategoriaRecetaService>();

            mockCategoriaReceta.Setup(d => d.ValidateCategoriaRecetaById(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                if (id == 1)
                    return true;
                else
                    return false;
            });

            return mockCategoriaReceta.Object;
        }
    }
}
