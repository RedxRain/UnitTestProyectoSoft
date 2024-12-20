using Application.Interfaces.Services;
using Moq;

namespace UniTestReceta.Mocks
{
    internal class MockDificultadServie
    {
        public IDificultadService ConfigureMockDificultadServie()
        {
            var mockDificultadServie = new Mock<IDificultadService>();

            mockDificultadServie.Setup(d => d.ValidateDificultadById(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                if (id == 1)
                    return true;
                else
                    return false;
            });

            return mockDificultadServie.Object;
        }
    }
}
