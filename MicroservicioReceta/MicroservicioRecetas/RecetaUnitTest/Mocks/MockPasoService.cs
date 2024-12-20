using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Moq;

namespace UniTestReceta.Mocks
{
    internal class MockPasoService
    {
        public IPasoService ConfigureMockPasoService()
        {
            var mockPasoService = new Mock<IPasoService>();

            mockPasoService.Setup(p => p.CreatePaso(It.IsAny<PasoRequest>(), It.IsAny<Guid>())).ReturnsAsync((PasoRequest pa, Guid Id) =>
            {
                return new PasoResponse
                {
                    Id = 1,
                    Descripcion = pa.Descripcion,
                    Foto = pa.Foto,
                    Orden = pa.Orden
                };
            });

            return mockPasoService.Object;
        }
    }
}
