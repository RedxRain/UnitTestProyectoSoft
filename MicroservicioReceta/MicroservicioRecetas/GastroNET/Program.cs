using Application.Interfaces.Commands;
using Application.Interfaces.Mappers;
using Application.Interfaces.Querys;
using Application.Interfaces.Services;
using Application.Mappers;
using Application.UseCases;
using Application.UseCases.CategoriaReceta;
//using Application.UseCases.IngredienteReceta;
using Application.UseCases.SDificultad;
using Application.UseCases.SPasos;
using Application.UseCases.SReceta;
using Application.UserService;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom
var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<RecetasContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDificultadService, DificultadService>();
builder.Services.AddScoped<IDificultadQuery, DificultadQuery>();

builder.Services.AddScoped<IRecetaService, RecetaService>();
builder.Services.AddScoped<IRecetaQuery, RecetaQuery>();
builder.Services.AddScoped<IRecetaCommand, RecetaCommand>();

builder.Services.AddScoped<IPasoService, PasoService>();
builder.Services.AddScoped<IPasoCommand, PasoCommand>();
builder.Services.AddScoped<IPasoQuery, PasoQuery>();

builder.Services.AddScoped<ICategoriaRecetaService, CategoriaRecetaService>();
builder.Services.AddScoped<ICategoriaRecetaQuery, CategoriaRecetaQuery>();

builder.Services.AddScoped<IIngredienteRecetaService, IngredienteRecetaService>();
builder.Services.AddScoped<IIngredienteRecetaQuery, IngredienteRecetaQuery>();
builder.Services.AddScoped<IIngredienteRecetaCommand, IngredienteRecetaCommand>();

builder.Services.AddScoped<IUserIngredienteService, UserIngredienteService>();

builder.Services.AddScoped<IRecetaMapper, RecetaMapper>();
builder.Services.AddScoped<IRecetaGetResponseMapper, RecetaGetResponseMapper>();
builder.Services.AddScoped<IRecetaDeleteMapper, RecetaDeleteMapper>();
builder.Services.AddScoped<ICategoriaRecetaMapper, CategoriaRecetaMapper>();
builder.Services.AddScoped<IDificultadMapper, DificultadMapper>();
builder.Services.AddScoped<IPasoMapper, PasoMapper>();
builder.Services.AddScoped<IIngredienteRecetaMapper, IngredienteRecetaMapper>();

//Inyección de Dependencias para los tokens.
builder.Services.AddScoped<JwtTokenService>();

//CORS deshabilitar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
