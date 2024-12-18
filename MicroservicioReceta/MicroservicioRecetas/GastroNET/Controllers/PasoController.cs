using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastroNET.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PasoController : Controller
    {
        private readonly IPasoService _service;

        public PasoController(IPasoService pasosService)
        {
            _service = pasosService;
        }

        //[HttpPost]
        //[ProducesResponseType(typeof(PasoResponse), 201)]
        //[ProducesResponseType(typeof(BadRequest), 400)]
        //[ProducesResponseType(typeof(BadRequest), 409)]
        //public async Task<IActionResult> CreatePaso(PasoRequest request)
        //{
        //    try
        //    {
        //        var result = await _service.CreatePaso(request);
        //        return new JsonResult(result) { StatusCode = 201 };
        //    }
        //    catch (ExceptionSintaxError ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
        //    }
        //    catch (Conflict ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
        //    }
        //}

        //[HttpPut("{Id}")]
        //[ProducesResponseType(typeof(PasoResponse), 200)]
        //[ProducesResponseType(typeof(BadRequest), 400)]
        //[ProducesResponseType(typeof(BadRequest), 404)]
        //[ProducesResponseType(typeof(BadRequest), 409)]
        //public async Task<IActionResult> UpdatePaso(int Id, PasoRequest paso)
        //{
        //    try
        //    {
        //        var result = await _service.UpdatePaso(paso, Id);
        //        return new JsonResult(result) { StatusCode = 200 };
        //    }
        //    catch (ExceptionSintaxError ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
        //    }
        //    catch (ExceptionNotFound ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
        //    }
        //    catch (Conflict ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
        //    }
        //}

        //[HttpDelete("{Id}")]
        //[ProducesResponseType(typeof(PasoResponse), 200)]
        //[ProducesResponseType(typeof(BadRequest), 400)]
        //[ProducesResponseType(typeof(BadRequest), 404)]
        //[ProducesResponseType(typeof(BadRequest), 409)]
        //public async Task<IActionResult> DeletePaso(int Id)
        //{
        //    try
        //    {
        //        var result = await _service.DeletePaso(Id);
        //        return new JsonResult(result) { StatusCode = 200 };
        //    }
        //    catch (ExceptionSintaxError ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
        //    }
        //    catch (ExceptionNotFound ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
        //    }
        //    catch (Conflict ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 409 };
        //    }
        //}


        //Lo mismo este, receta ya te trae todos los pasos

        //[HttpGet("{recetaId}")]
        //[ProducesResponseType(typeof(PasoResponse), 200)]
        //[ProducesResponseType(typeof(BadRequest), 400)]
        //[ProducesResponseType(typeof(BadRequest), 404)]
        //public async Task<IActionResult> GetPasosByRecetaId(Guid recetaId)
        //{
        //    try
        //    {
        //        var result = await _service.GetPasosByRecetaId(recetaId);
        //        return new JsonResult(result) { StatusCode = 200 };
        //    }
        //    catch (ExceptionSintaxError ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
        //    }
        //    catch (ExceptionNotFound ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
        //    }
        //}

        //El get de un paso solo no hace falta, en teoría los pasos vienen en el include de la receta :O


        //[HttpGet]
        //[ProducesResponseType(typeof(PasoResponse), 200)]
        //[ProducesResponseType(typeof(BadRequest), 400)]
        //[ProducesResponseType(typeof(BadRequest), 404)]
        //public async Task<IActionResult> GetPasoById(int id)
        //{
        //    try
        //    {
        //        var result = await _service.GetPasoById(id);
        //        return new JsonResult(result) { StatusCode = 200 };
        //    }
        //    catch (ExceptionSintaxError ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 400 };
        //    }
        //    catch (ExceptionNotFound ex)
        //    {
        //        return new JsonResult(new BadRequest { Message = ex.Message }) { StatusCode = 404 };
        //    }
        //}
    }
}