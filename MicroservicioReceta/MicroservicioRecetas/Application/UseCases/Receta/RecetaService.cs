﻿using Application.Exceptions;
using Application.Interfaces.Commands;
using Application.Interfaces.Mappers;
using Application.Interfaces.Querys;
using Application.Interfaces.Services;
using Application.Request;
using Application.Response;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Application.UseCases.SReceta
{
    public class RecetaService : IRecetaService
    {
        private readonly IRecetaQuery _query;
        private readonly IRecetaCommand _command;
        private readonly IPasoService _pasoService;
        private readonly ICategoriaRecetaService _categoriaService;
        private readonly IDificultadService _dificultadService;
        private readonly IIngredienteRecetaService _ingRecetaService;
        private readonly IUserIngredienteService _userIngredienteService;
        private readonly IRecetaMapper _recetaMapper;
        private readonly IRecetaDeleteMapper _recetaDeleteMapper;
        private readonly IRecetaGetResponseMapper _recetaGetResponseMapper;

        public RecetaService(IRecetaQuery recetaQuery, IRecetaCommand recetaCommand, IPasoService pasoService, ICategoriaRecetaService categoriaService, IDificultadService dificultadService, IIngredienteRecetaService ingredienteRecetaService, IUserIngredienteService userIngredienteService, IRecetaMapper recetaMapper, IRecetaDeleteMapper recetaDeleteMapper, IRecetaGetResponseMapper recetaGetResponseMapper)
        {
            _query = recetaQuery;
            _command = recetaCommand;
            _pasoService = pasoService;
            _categoriaService = categoriaService;
            _dificultadService = dificultadService;
            _ingRecetaService = ingredienteRecetaService;
            _userIngredienteService = userIngredienteService;
            _recetaMapper = recetaMapper;
            _recetaDeleteMapper = recetaDeleteMapper;
            _recetaGetResponseMapper = recetaGetResponseMapper;
        }

        //------ Metodos ABM ------
        public async Task<RecetaResponse> CreateReceta(RecetaRequest recetaRequest)
        {
            try
            {
                Receta recetaCreada = null;
                if (await VerifyAll(recetaRequest))
                {
                    TimeSpan tiempoPreparacion = await GetHorario(recetaRequest.TiempoPreparacion);
                    Receta receta = new Receta
                    {
                        CategoriaRecetaId = recetaRequest.CategoriaRecetaId,
                        DificultadId = recetaRequest.DificultadId,
                        UsuarioId = recetaRequest.UsuarioId,
                        Titulo = recetaRequest.Titulo,
                        FotoReceta = recetaRequest.FotoReceta,
                        Video = recetaRequest.Video,
                        TiempoPreparacion = tiempoPreparacion,
                        Topics = recetaRequest.Topics,
                        IngredentesReceta = new List<IngredienteReceta>(),
                        Pasos = new List<Paso>(),
                    };
                    recetaCreada = await _command.CreateReceta(receta);
                }
                foreach (PasoRequest paso in recetaRequest.ListaPasos) { await _pasoService.CreatePaso(paso, recetaCreada.RecetaId); }
                foreach (IngredienteRecetaRequest ingReceta in recetaRequest.ListaIngredienteReceta) { await _ingRecetaService.CreateIngredienteReceta(ingReceta, recetaCreada.RecetaId); }
                if (await _command.SaveChanges())
                {
                    throw new Exceptions.BadRequest("Error al publicar la receta");
                }
                return await _recetaMapper.CreateRecetaResponse(recetaCreada);
            }
            catch (ExceptionSintaxError e)
            {
                throw new ExceptionSintaxError("Error en la sintaxis de la receta a crear: " + e.Message);
            }
            catch (ExceptionNotFound e)
            {
                throw new ExceptionNotFound("No se pudo agregar la receta: " + e.Message);
            }
            catch (Exceptions.BadRequest e)
            {
                throw new Exceptions.BadRequest(e.Message);
            }
        }

        public async Task<RecetaResponse> UpdateReceta(RecetaRequest recetaRequest, Guid recetaId)
        {
            try
            {
                Receta unaReceta = null;
                if (!await VerifyRecetaId(recetaId)) { throw new ExceptionNotFound("El id de la receta no existe"); }
                if (await VerifyAll(recetaRequest))
                {
                    TimeSpan tiempoPreparacion = await GetHorario(recetaRequest.TiempoPreparacion);
                    unaReceta = await _command.UpdateReceta(recetaRequest, recetaId);
                    if (!await DeleteAllIngReceta(recetaId) && !await DeleteAllPasos(recetaId))
                    {
                        foreach (PasoRequest paso in recetaRequest.ListaPasos) { await _pasoService.CreatePaso(paso, recetaId); }
                        foreach (IngredienteRecetaRequest ingReceta in recetaRequest.ListaIngredienteReceta) { await _ingRecetaService.CreateIngredienteReceta(ingReceta, recetaId); }
                    }

                }
                if (await _command.SaveChanges()) { throw new Exceptions.BadRequest("Error al publicar la receta"); }
                return await _recetaMapper.CreateRecetaResponse(unaReceta);
            }
            catch (Conflict ex)
            {
                throw new Conflict("Error en la implementación a la base de datos: " + ex.Message);
            }
            catch (ExceptionNotFound ex)
            {
                throw new ExceptionNotFound("Error en la busqueda en la base de datos: " + ex.Message);
            }
            catch (ExceptionSintaxError ex)
            {
                throw new ExceptionSintaxError("Error en la sintaxis: " + ex.Message);
            }
        }

        public async Task<RecetaDeleteResponse> DeleteReceta(Guid id)
        {
            try
            {
                Receta recetaToDelete = null;
                if (!await VerifyRecetaId(id)) { throw new ExceptionNotFound("El id no existe"); }
                if (!await DeleteAllIngReceta(id) && !await DeleteAllPasos(id))
                { recetaToDelete = await _command.DeleteReceta(await _query.GetRecetaById(id)); }
                if (await _command.SaveChanges()) { throw new Exceptions.BadRequest("Error al eliminar la receta"); }
                return await _recetaDeleteMapper.CreateRecetaDeleteResponse(recetaToDelete);
            }
            catch (ExceptionNotFound ex) { throw new ExceptionNotFound("Error en la búsqueda de la receta: " + ex.Message); }
            catch (Conflict ex) { throw new Conflict("Error en la base de datos: " + ex.Message); }
        }

        public async Task<RecetaResponse> GetRecetaById(Guid id)
        {
            try
            {
                var receta = await _query.GetRecetaById(id);
                if (receta != null)
                {
                    return await _recetaMapper.CreateRecetaResponse(receta);
                }
                else
                {
                    throw new ExceptionNotFound("No existe ninguna receta con ese ID");
                }
            }
            catch (ExceptionNotFound e)
            {
                throw new ExceptionNotFound("Error en la búsqueda: " + e.Message);
            }
        }

        public async Task<List<RecetaResponse>> GetListRecetas()
        {
            var recetas = await _query.GetListRecetas();
            var recetasResponse = new List<RecetaResponse>();

            foreach (var receta in recetas)
            {
                recetasResponse.Add(await _recetaMapper.CreateRecetaResponse(receta));
            }
            return recetasResponse;
        }

        public async Task<List<RecetaGetResponse>> GetRecetaByFilter(string? tituloIngredienteTopic, int dificultad, int categoria)
        {
            try
            {
                List<Receta> listaFiltrada;

                if (string.IsNullOrEmpty(tituloIngredienteTopic) && dificultad == 0 && categoria == 0)
                {
                    listaFiltrada = await _query.GetListRecetas();
                }
                else
                {
                    listaFiltrada = (await _query.GetRecetasByFilters(tituloIngredienteTopic, dificultad, categoria));
                }

                if (listaFiltrada.IsNullOrEmpty())
                { throw new ExceptionNotFound("No se encontro ninguna Receta"); }

                var resultados = await Task.WhenAll(listaFiltrada.Select(async unaReceta =>
                        await _recetaGetResponseMapper.CreateRecetaGetResponse(unaReceta)
                    ));

                return resultados.ToList();
            }
            catch (ExceptionNotFound ex) { throw new ExceptionNotFound("Error en la búsqueda: " + ex.Message); }
            catch (ExceptionSintaxError ex) { throw new ExceptionSintaxError("Error en la búsqueda: " + ex.Message); }
        }

        private async Task<bool> VerifyAll(RecetaRequest recetaRequest)
        {
            if (!await VerifyDificultadId(recetaRequest.DificultadId)) { throw new ExceptionNotFound("No existe ninguna categoría para ese ID"); }
            if (!VerifyInt(recetaRequest.DificultadId)) { throw new ExceptionSintaxError("Formato érroneo para el id de dificultad, pruebe un entero"); }

            if (!await VerifyCategoriaRecetaId(recetaRequest.CategoriaRecetaId)) { throw new ExceptionNotFound("No existe ninguna categoría para ese ID"); }
            if (!VerifyInt(recetaRequest.CategoriaRecetaId)) { throw new ExceptionSintaxError("Formato érroneo para el id de categoriaReceta, pruebe un entero"); }

            if (!await VerifyTitleLength(recetaRequest.Titulo)) { throw new ExceptionSintaxError("El titulo es demasiado extenso"); }
            if (!await VerifyFotoRecetaLenght(recetaRequest.FotoReceta)) { throw new ExceptionSintaxError("El url de la imagen es demasiado extenso"); }
            if (!await VerifyVideoLenght(recetaRequest.Video)) { throw new ExceptionSintaxError("El url del video es demasiado extenso"); }

            if (!await VerifyListIsNotEmpty(recetaRequest.ListaIngredienteReceta)) { throw new ExceptionSintaxError("No hay ingredientes en la lista, por favor ingrese al menos uno"); }
            if (!await VerifyListIsNotEmpty(recetaRequest.ListaPasos)) { throw new ExceptionSintaxError("No hay pasos ingresados, por favor ingrese al menos uno"); }

            return true;
        }

        // Validadores de enteros
        private bool VerifyInt(int entero)
        { return (int.TryParse(entero.ToString(), out entero)); }
        private async Task<bool> VerifyRecetaId(Guid recetaId)
        { return (await _query.GetRecetaById(recetaId) != null); }

        private async Task<bool> VerifyDificultadId(int dificultadId)
        { return (await _dificultadService.ValidateDificultadById(dificultadId)); }
        private async Task<bool> VerifyCategoriaRecetaId(int categoriaRecetaId)
        { return (await _categoriaService.ValidateCategoriaRecetaById(categoriaRecetaId)); }

        // Validadores de strings
        private async Task<bool> VerifyTitleLength(string title)
        { return (await _query.GetTitleLength() > title.Length); }

        private async Task<bool> VerifyFotoRecetaLenght(string fotoreceta)
        { return (await _query.GetFotoRecetaLength() > fotoreceta.Length); }
        private async Task<bool> VerifyVideoLenght(string video)
        { return (await _query.GetVideoLenght() > video.Length); }

        //Validador de listas
        private Task<bool> VerifyListIsNotEmpty<T>(List<T> lista)
        { return Task.FromResult(lista.Count() > 0); }

        //Deletes
        private async Task<bool> DeleteAllIngReceta(Guid recetaId)
        { return await _ingRecetaService.DeleteAllIngRecetaWhitRecetaId(recetaId); }
        private async Task<bool> DeleteAllPasos(Guid recetaId)
        { return await _pasoService.DeleteAllPasosWhitRecetaId(recetaId); }

        //Otros
        private Task<TimeSpan> GetHorario(string tiempoPreparacion)
        {
            if (tiempoPreparacion.Length == 5 &&
                int.TryParse(tiempoPreparacion.Substring(0, 2), out int horas) &&
                int.TryParse(tiempoPreparacion.Substring(3, 2), out int minutos) &&
                tiempoPreparacion[2] == ':')
            { return Task.FromResult(new TimeSpan(horas, minutos, 0)); }
            else
            { throw new ExceptionSintaxError("Formato erróneo para el horario de la receta, pruebe usando hh:mm"); }
        }


    }
}
