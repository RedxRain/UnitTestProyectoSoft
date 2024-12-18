using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GastroNET.Controllers
{
    public class IngredienteRecetasController : Controller
    {
        private readonly IIngredienteRecetaService _service;

        public IngredienteRecetasController(IIngredienteRecetaService service)
        {
            _service = service;
        }
    }
}
