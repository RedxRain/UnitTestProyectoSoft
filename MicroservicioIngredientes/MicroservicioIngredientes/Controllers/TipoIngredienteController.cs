using Application.Exceptions;
using Application.Interfaces;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioIngredientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIngredienteController : ControllerBase
    {
        private readonly ITipoIngredienteService _service;

        public TipoIngredienteController(ITipoIngredienteService service)
        {
            _service = service;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(TipoIngredienteResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public async Task<IActionResult> GetByTipoIngrediente(int Id)
        {
            try
            {
                var result = await _service.GetByTipoIngrediente(Id);
                return new JsonResult(result);
            }

            catch (BadRequestException ex)
            { return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 }; }
        }
    }
}
