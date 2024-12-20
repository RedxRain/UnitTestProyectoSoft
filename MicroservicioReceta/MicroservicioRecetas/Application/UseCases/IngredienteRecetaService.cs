using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Interfaces.Mappers;
using Application.Interfaces.Querys;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Domain.Entities;

namespace Application.UseCases
{
    public class IngredienteRecetaService : IIngredienteRecetaService
    {
        private readonly IIngredienteRecetaQuery _query;
        private readonly IIngredienteRecetaCommand _command;
        private readonly IUserIngredienteService _UServIngrediente;
        private readonly IIngredienteRecetaMapper _mapper;
        public IngredienteRecetaService(IIngredienteRecetaQuery query, IIngredienteRecetaCommand command, IUserIngredienteService uServIngrediente, IIngredienteRecetaMapper mapper)
        {
            _query = query;
            _command = command;
            _UServIngrediente = uServIngrediente;
            _mapper = mapper; //Ver si tira error
        }
        public async Task<IngredienteRecetaResponse> CreateIngredienteReceta(IngredienteRecetaRequest request, Guid recetaId)
        {
            try
            {
                //El validador de ingrecetaId tiene que venir por conexión con el microservicio ingredientes
                //Validador de si existe la receta id
                dynamic ingrediente = _UServIngrediente.GetByID(request.ingredienteId);
                if (await VerifyAll(request, recetaId))
                {
                    IngredienteReceta unIngReceta = new IngredienteReceta
                    {
                        RecetaId = recetaId,
                        IngredienteId = request.ingredienteId,
                        cantidad = request.cantidad
                    };

                    return await _mapper.GetIngredienteRecetaResponse(await _command.CreateIngredienteReceta(unIngReceta));
                }
                throw new Exception("Ocurrió un error inesperado");

            }
            catch (ExceptionSintaxError ex)
            {
                throw new ExceptionSintaxError(ex.Message);
            }
            catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound(ex.Message + request.ingredienteId);
            }
            catch (Conflict ex)
            {
                //si ya existe ese ingrediente en la receta, lo rajamos
                throw new Conflict(ex.Message);
            }


        }

        public async Task<bool> DeleteAllIngRecetaWhitRecetaId(Guid recetaId)
        {
            try
            {
                return (await _command.DeleteAllIngRecetaByRecetaId(recetaId));
            }
            catch (Exceptions.BadRequest)
            { throw new Exceptions.BadRequest("Error al eliminar los ingredientes"); }
        }

        public async Task<IngredienteRecetaDeleteResponse> DeleteIngredienteReceta(int ingRecetaId)
        {
            try
            {
                IngredienteReceta ingReceta = await _query.GetIngRecetaById(ingRecetaId);
                if (ingReceta == null) { throw new ExceptionNotFound("No existe ese ingrediente en la receta solicitada"); }
                else
                {
                    return await _mapper.GetIngredienteDeleteResponse(await _command.DeleteIngredienteReceta(ingReceta));
                }
            }
            catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound("Ocurrió un error en la búsqueda: " + ex.Message);
            }
            catch (ExceptionSintaxError ex)
            {
                throw new ExceptionSintaxError("Hubo un error en la sintaxis: " + ex.Message);
            }
        }

        public async Task<int> GetIngredienteRecetaBy(Guid recetaId, int ingredienteId)
        {
            return await _query.GetIngRecetaByRecetaId(recetaId, ingredienteId);
        }




        //----- Metodos privados -----

        //------ Validadores -----
        private async Task<bool> VerifyAll(IngredienteRecetaRequest request, Guid? recetaId)
        {
            //Solo validadores de IngredienteReceta
            //ver los validadores de ints
            //Ver este porque tiene que lo que conecta al microservicio :D
            //if (request.ingredienteId == 0) { throw new ExceptionNotFound("No existe el ingrediente"); }
            //if (_UServIngrediente.GetByID(request.ingredienteId))

            // ^--- Con la llamada al microservicio ingrediente, pedir el nombre para mostrar el ingrediente que es menor a 0 :D

            // ^--- Con la llamada al microservicio ingrediente, pedir el nombre para mostrar el ingrediente que se está repitiendo :D
            //if (ingRecetaId > 0 && _query.GetIngRecetaById(ingRecetaId) == null) { throw new ExceptionNotFound("No existe el ingrediente"); }
            //En blanco: Tiene  atributos: ingrediente id, cantidad!
            //Crear IngredienteReceta ---> Validar que cantidad sea >0, que ingrediente exista y que este no se repita pero este podemos manejarlo dependiendo de que devuelva
            //Update Ingrediente receta ---> Validar que cantidad sea >0, que receta id exista, que no se repitan ingredientes en los pasos
            //Para el update el profesor sugirió que borre al pingo toda la lista y se cree de 0
            if (request.cantidad < 1) { throw new ExceptionSintaxError("La cantidad no puede ser menor a 1"); }
            if (recetaId != null && await _query.ExistIngredienteInIngReceta((Guid)recetaId, request.ingredienteId)) { throw new Conflict("Ya existe el ingrediente en la receta"); }

            return true;
        }
    }
}
