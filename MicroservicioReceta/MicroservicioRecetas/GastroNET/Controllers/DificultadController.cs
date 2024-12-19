using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace GastroNET.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DificultadController : Controller
    {
        private readonly IDificultadService _service;

        public DificultadController(IDificultadService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DificultadResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]

        public async Task<IActionResult> GetListDificultad()
        {
            try
            {
                var result = await _service.GetListDificultad();
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (ExceptionSintaxError ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (ExceptionNotFound ex)
            {
                return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
            }
        }
    }
}
