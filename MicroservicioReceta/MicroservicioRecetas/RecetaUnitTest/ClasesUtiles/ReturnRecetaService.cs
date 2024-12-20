using Application.Interfaces.Mappers;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.UseCases.SReceta;
using Moq;
using UniTestReceta.Mocks;

namespace UniTestReceta.ClasesUtiles
{
    internal class ReturnRecetaService
    {
        public IRecetaService RetornarRecetaService()
        {
            var mockUserIngredienteService = new Mock<IUserIngredienteService>();
            var mockIngRecetaService = new Mock<IIngredienteRecetaService>();
            RecetaGetResponseMapper recetaGetResponseMapper = new RecetaGetResponseMapper(new DificultadMapper(), new CategoriaRecetaMapper());

            return new RecetaService((new MockRecetaQuery()).ConfigureMockRecetaQuery(),
                                          (new MockRecetaCommand()).ConfigureMockRecetaCommand(),
                                          (new MockPasoService()).ConfigureMockPasoService(),
                                          (new MockCategoriaReceta()).ConfigureMockCategoriaReceta(),
                                          (new MockDificultadServie()).ConfigureMockDificultadServie(),
                                          mockIngRecetaService.Object,
                                          mockUserIngredienteService.Object,
                                          (new MockRecetaMapper()).ConfigureMockRecetaMapper(),
                                          (new MockRecetaDeleteMapper()).ConfigureMockRecetaDeleteMapper(),
                                          recetaGetResponseMapper);
        }
    }
}
