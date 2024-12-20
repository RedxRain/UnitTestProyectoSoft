using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Response;
using UniTestReceta.ClasesUtiles;

namespace RecetaUnitTest
{
    public class RecetaUnitTest
    {
        private readonly IRecetaService _service;
        private readonly ReturnRecetaRequest _returnRecetaRequest = new ReturnRecetaRequest();
        public RecetaUnitTest()
        {
            _service = new ReturnRecetaService().RetornarRecetaService();
        }

        [Theory]
        [InlineData(1, 1, "nioqui", "video", "foto", "03:30", true)]
        [InlineData(2, 1, "nioqui", "video", "foto", "03:30", false)]
        [InlineData(1, 2, "nioqui", "video", "foto", "03:30", false)]
        [InlineData(1, 1, "nioquiiiiiiiiiii", "video", "foto", "03:30", false)]
        [InlineData(1, 1, "nioqui", "videooooooooo", "foto", "03:30", false)]
        [InlineData(1, 1, "nioqui", "video", "fotoooooooooo", "03:30", false)]
        [InlineData(1, 1, "nioqui", "video", "foto", "0300:30", false)]
        public async Task CreateRecetaTest(int categoriaRecetaId, int dificultadId, string titulo, string video, string foto, string horario,bool flag)
        {
            //Arrange
            var request = _returnRecetaRequest.RetornarRecetaRequest(categoriaRecetaId, dificultadId, titulo, video, foto, horario);
              
            if (flag)
            {
                //Act
                var result = await _service.CreateReceta(request);
                //Assert
                Assert.Equal(titulo, result.Titulo);
                Assert.Equal(dificultadId, result.Dificultad.Id);
            }
            else
            {
                //Act
                var excepcion = await Assert.ThrowsAnyAsync<Exception>(()=>_service.CreateReceta(request));
                //Assert
                Assert.NotNull(excepcion);
                Assert.Contains("la receta", excepcion.Message);
            }            
        }

        [Theory]
        [InlineData(1, 1, "nioqui", "video", "foto", "03:30", "311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", true)]
        [InlineData(2, 1, "nioqui", "video", "foto", "03:30", "311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", false)]
        [InlineData(1, 2, "nioqui", "video", "foto", "03:30", "311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", false)]
        [InlineData(1, 1, "nioquiiiiiiiiiii", "video", "foto", "03:30", "311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", false)]
        [InlineData(1, 1, "nioqui", "videooooooooo", "foto", "03:30", "311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", false)]
        [InlineData(1, 1, "nioqui", "video", "fotoooooooooo", "03:30", "311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", false)]
        [InlineData(1, 1, "nioqui", "video", "foto", "0300:30", "311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", false)]
        [InlineData(1, 1, "nioqui", "video", "foto", "03:30", "00000000-0000-0000-0000-000000000000", false)]
        public async Task UpdateRecetaTest(int categoriaRecetaId, int dificultadId, string titulo, string video, string foto, string horario, string id, bool flag)
        {
            //Arrange
            var request = _returnRecetaRequest.RetornarRecetaRequest(categoriaRecetaId, dificultadId, titulo, video, foto, horario);
            Guid i = Guid.Parse(id);

            if (flag)
            {
                //Act
                var result = await _service.UpdateReceta(request,i);
                //Assert
                Assert.Equal(request.CategoriaRecetaId, categoriaRecetaId);
                Assert.Equal(request.DificultadId, dificultadId);
                Assert.Equal(request.Titulo, titulo);
                Assert.Equal(request.Video, video);
            }
            else
            {
                //Act
                var excepcion = await Assert.ThrowsAnyAsync<Exception>(() => _service.UpdateReceta(request, i));
                //Assert
                Assert.NotNull(excepcion);
                Assert.Contains("Error", excepcion.Message);
            }
        }

        [Theory]
        [InlineData(null, 0, 0, true)]
        [InlineData("Arroz", 4, 4, false)]
        [InlineData("Ñoquis", 1, 1, true)]
        [InlineData(null, 1, 0, true)]
        public async Task BusquedaTest(string? TopicNombre, int dificultad, int categoria, bool flag)
        {
            //Act y Assert
            if (flag)
            {
                var result = await _service.GetRecetaByFilter(TopicNombre, dificultad, categoria);
                Assert.IsType<List<RecetaGetResponse>>(result);
            }
            else
            {
                var excepcion = await Assert.ThrowsAsync<ExceptionNotFound>(() => _service.GetRecetaByFilter(TopicNombre, dificultad, categoria));
                Assert.IsType<ExceptionNotFound>(excepcion);
            }
        }

        [Theory]
        [InlineData("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", true)]
        [InlineData("00000000-0000-0000-0000-000000000000", false)]
        public async Task GetByIdTest(string id, bool flag)
        {
            //Arrange
            Guid i = Guid.Parse(id);
            //Act y Assert
            if (flag)
            {
                var result = await _service.GetRecetaById(i);
                Assert.Equal(i, result.Id);
            }
            else
            {
                var excepcion = await Assert.ThrowsAsync<ExceptionNotFound>(() => _service.GetRecetaById(i));
                Assert.IsType<ExceptionNotFound>(excepcion);
            }
        }

        [Theory]
        [InlineData("311FF725-CEFB-4BE9-C8C7-08DD1A1015F8", true)]
        [InlineData("00000000-0000-0000-0000-000000000000", false)]
        public async Task DeleteRecetaTest(string id, bool flag)
        {
            //Arrange            
            Guid i = Guid.Parse(id);
            
            if (flag)
            {
                //Act
                var result = await _service.DeleteReceta(i);
                //Assert
                Assert.Equal(i, result.id);
            }
            else
            {
                //Act
                var excepcion = await Assert.ThrowsAsync<ExceptionNotFound>(() => _service.DeleteReceta(i));
                //Assert
                Assert.IsType<ExceptionNotFound>(excepcion);
            }
        }
        [Fact]
        public async Task GetListRecetasTest()
        {
            //Act
            var result = await _service.GetListRecetas();
            //Assert
            Assert.IsType<List<RecetaResponse>>(result);
            Assert.NotEmpty(result);
        }
    }
}