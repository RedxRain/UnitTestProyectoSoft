using Application.Exceptions;
using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioIngredientes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        private readonly IIngredienteService _service;

        public IngredienteController(IIngredienteService service)
        {
            _service = service;
        }

        [HttpGet("ById/{Id}")]
        [ProducesResponseType(typeof(IngredienteResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        [ProducesResponseType(typeof(BadRequest), 401)]
        public async Task<IActionResult> GetByID(int Id)
        {
            try
            {
                var result = await _service.GetById(Id);
                return new JsonResult(result) { StatusCode = 200 };
            }

            catch (BadRequestException ex)
            { return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 }; }
            catch (NotFoundException ex)
            { return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 }; }
            catch (UnauthorizedException ex)
            { return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 401 }; }
        }

        [HttpGet("ByName/{Name}")]
        [ProducesResponseType(typeof(List<IngredienteResponse>), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 401)]
        public async Task<IActionResult> GetByName(string Name)
        {
            try
            {
                var result = await _service.GetByName(Name);
                return new JsonResult(result) { StatusCode = 200 };
            }

            catch (BadRequestException ex)
            { return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 }; }
            catch (NotFoundException ex)
            { return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 }; }
            catch (UnauthorizedException ex)
            { return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 401 }; }
        }
    }
}
