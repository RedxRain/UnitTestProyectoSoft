using Application.Exceptions;
using Application.Interfaces;
using Application.Response;
using Application.UseCase;
using Domain.Entities;
using MicroservicioIngredientes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IngredientesUnitTest
{
    public class IngredientesUnitTest
    {
        [Fact]
        public async Task GetByIdOk()
        {
            //Arrange
            var mockService = new Mock<IIngredienteService>();
            var _controller = new IngredienteController(mockService.Object);
            int i = 12;

            mockService.Setup(s => s.GetById(i)).ReturnsAsync(
                new IngredienteResponse
                {
                    Id = 12,
                    Name = "Aceitunas",
                    TipoIngrediente = new TipoIngredienteGetResponse
                    {
                        Id = 1,
                        Name = "Fruta"
                    },
                    TipoMedida = new TipoMedidaGetResponse
                    {
                        Id = 1,
                        Name = "Unidad"
                    }
                }
                );

            //Act
            var result = await _controller.GetByID(i);
            //Assert
            var statusResult = Assert.IsType<JsonResult>(result);
            Assert.Equal(200, statusResult.StatusCode);
        }

        [Fact]
        public async Task GetByNameShouldThrowNotFoundException()
        {
            //Arrange
            var mockQuery = new Mock<IIngredienteQuery>();
            var mockCommand = new Mock<IIngredienteCommand>();
            var _service = new IngredienteService (mockCommand.Object, mockQuery.Object);
            string nom = "Lechuga";
            mockQuery.Setup(q => q.GetAll().Result).Returns(new List<Ingrediente>());

            //Act

            //Assert
            var result = await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByName(nom));
            Assert.IsType<NotFoundException>(result);
        }
    }
}