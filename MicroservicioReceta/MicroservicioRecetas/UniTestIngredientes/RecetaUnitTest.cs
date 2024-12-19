using Application.Interfaces.Commands;
using Application.Interfaces.Mappers;
using Application.Interfaces.Querys;
using Application.Interfaces.Services;
using Application.Response;
using Application.UseCases.SReceta;
using GastroNET.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UniTestIngredientes
{
    public class RecetaUnitTest
    {
        private readonly IRecetaService _service;

        public RecetaUnitTest()
        {
            var mockQuery = new Mock<IRecetaQuery>();
            var mockCommand = new Mock<IRecetaCommand>();
            var mockPasoService = new Mock<IPasoService>();
            var mockCategoriaService = new Mock<ICategoriaRecetaService>();
            var mockDificultadService = new Mock<IDificultadService>();
            var mockIngRecetaService = new Mock<IIngredienteRecetaService>();
            var mockUserIngredienteService = new Mock<IUserIngredienteService>();
            var mockRecetaMapper = new Mock<IRecetaMapper>();
            var mockRecetaDeleteMapper = new Mock<IRecetaDeleteMapper>();
            var mockRecetaGetResponseMapper = new Mock<IRecetaGetResponseMapper>();
            _service = new RecetaService (mockQuery.Object,
                                          mockCommand.Object,
                                          mockPasoService.Object,
                                          mockCategoriaService.Object,
                                          mockDificultadService.Object,
                                          mockIngRecetaService.Object,
                                          mockUserIngredienteService.Object,
                                          mockRecetaMapper.Object,
                                          mockRecetaDeleteMapper.Object,
                                          mockRecetaGetResponseMapper.Object);
        }

        [Fact]
        public async Task BusquedaTest()
        {
            //Arrange
            //Act
            var result = _service.GetRecetaByFilter(null, 1, 0);
            //Assert
            Assert.Equal(typeof(Task<List<RecetaGetResponse>>), result.GetType());
        }

        [Fact]
        public async Task GetByIdTest()
        {
            //Arrange
            Guid i = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8");
            //Act
            var result = _service.GetRecetaById(i);
            //Assert
            Assert.IsType<Task<RecetaResponse>>(result);
        }

        [Fact]
        public async Task DeleteRecetaTest()
        {
            //Arrange
            var mockService = new Mock<IRecetaService>();
            RecetaController _controller = new RecetaController(mockService.Object);
            Guid i = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8");
            mockService.Setup(s => s.DeleteReceta(It.IsAny<Guid>())).ReturnsAsync(new RecetaDeleteResponse
            {
                id = Guid.Parse("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8"),
                titulo = "RecetaBorrada"
            });
            //Act
            var result = await _controller.DeleteReceta(i);
            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var response = Assert.IsType<RecetaDeleteResponse>(jsonResult.Value);
            Assert.Equal(i, response.id);
        }
    }
}